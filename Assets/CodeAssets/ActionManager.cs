using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;
using HyperCard;

public class ActionManager : MonoBehaviour
{
    private Deck deck => ServiceLocator.GetGameStateTracker().Deck;

    GameState gameState;

    public Image ArmageddonCounterImage;

    // Used to end the current action/start the next delayed action
    public bool IsCurrentActionFinished { get; set; }

    public List<DelayedAction> actionsQueue = new List<DelayedAction>();

    public void PromptPossibleUpgradeOfCard(AbstractCard beforeCard)
    {
        QueuedActions.ImmediateAction(() =>
        {
            var afterCard = beforeCard.CopyCard();
            afterCard.Upgrade();
            ServiceLocator.GetUiStateManager().SwitchToCardModificationScreen(new ShowingCardModificationMessage
            {
                afterCard = afterCard,
                beforeCard = beforeCard,
                CanCancel = true,
                ThingToDoUponConfirmation = () =>
                {
                    beforeCard.Upgrade(); 
                    ServiceLocator.GetCardAnimationManager().RefreshCardAppearance(beforeCard);
                },
                Name = "Modify card? ",
                TitleMessage = "Modify card? "
            });
        });
    }

    public void DoAThing(Action action)
    {
        QueuedActions.ImmediateAction(action);
    }


    public void UpgradeCard(AbstractCard card)
    {
        QueuedActions.ImmediateAction(() =>
        {
            card.Upgrade();
        });

    }

    public Image StabilityImage;
    public Image ProductionImage;
    public Image ScienceImage;
    public Image CoinImage;

    internal void PurgeCardFromDeck(AbstractCard card, QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {

            var position = deck.PurgeCardFromDeck(card.Id);
            if (position == CardPosition.HAND)
            {
                // Animate dissolving
                var movement = ServiceLocator.GetCardAnimationManager().GetCardMovementBehavior(card);
                movement.DissolveAndDestroyCard();
                ServiceLocator.GetCardAnimationManager().RemoveHypercardFromHand(card);

            }
        }, queueingType);
    }

    public void DrawCards(int n = 1, QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {
            var hand = deck.DrawNextNCards(n);
            ServiceLocator.GetCardAnimationManager().AddHypercardsToHand(hand.Select(item => item.CreateHyperCard()).ToList());
        }, queueingType);
    }

    internal void AddCardToHand(AbstractCard abstractCard, QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {

            ServiceLocator.GetGameStateTracker().Deck.Hand.Add(abstractCard);
            ServiceLocator.GetCardAnimationManager().AddHypercardsToHand(new List<Card> { abstractCard.CreateHyperCard() });
        },queueingType);
    }

    UiAnimationHandler animationHandler;

    // Use this for initialization
    void Start()
    {
        gameState = ServiceLocator.GetGameStateTracker();
        gameObject.AddComponent<UiAnimationHandler>();
        animationHandler = this.GetComponent<UiAnimationHandler>();
    }

    public void ApplyDefense(AbstractBattleUnit unit, int amountOfDefense)
    {
        var cardsThatCanBeSelected = ServiceLocator.GameLogic().GetSelectableCardsFromScience();
        QueuedActions.ImmediateAction(() =>
        {
            // todo;
        });
    }

    public void PromptCardReward()
    {
        var cardsThatCanBeSelected = ServiceLocator.GameLogic().GetSelectableCardsFromScience();
        QueuedActions.DelayedAction("Choose New Card For Deck", () => {
            ServiceLocator.GetUiStateManager().SwitchToUiState(new ShowSelectCardToAddScreenMessage { CardsToShow = cardsThatCanBeSelected.ToList() });
        });
    }

    public void AddCardToDeck(AbstractCard protoCard, QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {

            var persistentDeckList = ServiceLocator.GetGameStateTracker().Deck.PersistentDeckList;
            if (persistentDeckList.Where(item => item.Id == protoCard.Id).Any())
            {
                throw new Exception("Attempted to add card to deck that already had the same ID as a card in the deck already: " + protoCard.Name);
            }
            Debug.Log("Added card to deck: " + protoCard.Name);

            ServiceLocator.GetCardAnimationManager().RunCreateNewCardAndAddToDiscardPileAnimation(protoCard);
            // Animate: Card created in center of screen, wait for a second, and shrinks while going down to the deck.
            ServiceLocator.GetGameStateTracker().Deck.DiscardPile.Add(protoCard);
        }, queueingType);
    }

    public void DeployCardSelectedIfApplicable(Card card, QueueingType queueingType = QueueingType.TO_BACK
        )
    {
        QueuedActions.ImmediateAction(() =>
        {
#pragma warning disable CS0618 // Type or member is obsolete
            gameState.DeployCardSelectedIfApplicable(card);
#pragma warning restore CS0618 // Type or member is obsolete
        }, queueingType);
    }


    public void DiscardCard(AbstractCard protoCard,  QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {

            gameState.Deck.MoveCardToPile(protoCard, CardPosition.DISCARD);
            ServiceLocator.GetCardAnimationManager().MoveCardToDiscardPile(protoCard, assumedToExistInHand: false);
        }, queueingType);
    }

    public void ExpendCard(AbstractCard protoCard, QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {
            gameState.Deck.MoveCardToPile(protoCard, CardPosition.EXPENDED);
            ServiceLocator.GetCardAnimationManager().MoveCardToDiscardPile(protoCard, assumedToExistInHand: false);
        }, queueingType);
    }

    public void DiscardHand()
    {
        QueuedActions.ImmediateAction(() =>
        {

            var hand = ServiceLocator.GetGameStateTracker().Deck.Hand.ToList();
            foreach (var card in hand)
            {
                DiscardCard(card);
            }
        });
    }

    public void ModifyCoin(int amount, QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {

            if (amount == 0)
            {
                return;
            }
            gameState.ModifyCoin(amount);
            animationHandler.PulseAndFlashElement(ArmageddonCounterImage);
        }, queueingType);
    }


    public void ModifyCard(AbstractCard card, Action<AbstractCard> actionToPerform, QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {
            var beforeCard = card.CopyCard();
            var afterCard = card.CopyCard();
            actionToPerform(card);
            actionToPerform(afterCard);
            var cardState = ServiceLocator.GetGameStateTracker().Deck.GetCardPosition(card.Id);
            ServiceLocator.GetUiStateManager().SwitchToCardModificationScreen(new ShowingCardModificationMessage
            {
                beforeCard = beforeCard,
                afterCard = afterCard,
                TitleMessage = $"Card Changed Notification (this one's in your {cardState} pile"
            });
            ServiceLocator.GetCardAnimationManager().RefreshCardAppearance(card);
        }, queueingType);
    }


    BattleTurnEndActions turnEndActions = new BattleTurnEndActions();
    public void EndBattleTurn()
    {
        QueuedActions.ImmediateAction(() =>
        {
            turnEndActions.EndTurn();
        });
    }

    public void OnceFinished(Action action)
    {
        QueuedActions.ImmediateAction(action);
    }

    [Obsolete("Use modifyCoin instead")]
    internal void ModifyCommerce(int commerce)
    {
        ModifyCoin(commerce);
    }


    /// <summary>
    /// Prompts the player to discard a card.
    /// </summary>
    public void PromptDiscardEvent(int numCardsToDiscard)
    {
        QueuedActions.DelayedAction("PromptDiscardEvent", () =>
        {
            ServiceLocator.GetUiStateManager().PromptPlayerForCardSelection(new DiscardCardUiState());
        });
    }

    public void Update()
    {
        var firstAction = this.actionsQueue.FirstOrDefault();
        if (firstAction != null)
        {
            if (!firstAction.IsStarted)
            {
                firstAction.IsStarted = true;
                try
                {
                    firstAction.onStart();
                } catch (Exception e)
                {
                    Debug.LogError(firstAction.stackTrace.ToString());
                    throw;
                }
            }
            if (firstAction.IsFinished())
            {
                IsCurrentActionFinished = false; // the current action isn't started yet.
                actionsQueue.PopFirstElement(); // remove the action to get a new current action.
                Console.Out.Write("Finished action on queue!");
            }
        }
    }
    #region RivalUnits

    public void CreateUnitAtBattleUnitHolder(AbstractBattleUnit unit)
    {
        /*
        var copyOfUnit = unit.Clone();
        var unitPrefab = ServiceLocator.GetRivalUnitPrefab().Spawn(ServiceLocator.GetUnitFolder());
        unitPrefab.RivalUnitEntity = copyOfUnit;
        copyOfUnit.Prefab = unitPrefab;
        copyOfUnit.Faction = faction;
        copyOfUnit.TileLocation = tile;
        unitPrefab.transform.position = tile.ToTile().HexPrefab.transform.position;
        gameState.RivalUnits.Add(copyOfUnit);
        */
    }

    public void AttackUnitForDamage(AbstractBattleUnit unit, int damage)
    {
        unit.CurrentHp -= damage;
        if (unit.CurrentHp <= 0)
        {
            DestroyUnit(unit);
        }

    }

    public void DestroyUnit(AbstractBattleUnit unit)
    {
        QueuedActions.ImmediateAction(() =>
        {
            unit.CorrespondingPrefab.gameObject.Despawn();
        });
    }

    public void ChangeUnit(AbstractBattleUnit unit, Action<AbstractBattleUnit> action)
    {
        QueuedActions.ImmediateAction(() =>
        {
            action(unit);
        });
    }

    private IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed)
    {
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.position = b;
        ServiceLocator.GetActionManager().IsCurrentActionFinished = true;
    }

    #endregion
}

public enum ModifyType
{
    SET, ADD_VALUE
}