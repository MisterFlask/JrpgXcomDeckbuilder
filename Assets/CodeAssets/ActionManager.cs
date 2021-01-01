using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;
using HyperCard;

[RequireComponent(typeof(Log))]
public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance;

    public ActionManager()
    {
        Instance = this;
    }

    private BattleDeck deck => ServiceLocator.GetGameStateTracker().Deck;

    GameState gameState => ServiceLocator.GetGameStateTracker();

    // Used to end the current action/start the next delayed action
    public bool IsCurrentActionFinished { get; set; }

    public List<BasicDelayedAction> actionsQueue = new List<BasicDelayedAction>();

    public string GetQueueActionsDebugLogs()
    {
        var actionStrings = actionsQueue.Select(item => $"[id={item.ActionId}, started={item.IsStarted}]");
        return $"ACTION MANAGER QUEUE STATE: [[{string.Join("|", actionStrings)}]]  ; total count of items in queue = ${actionStrings.Count()}";

    }

    public void AddToFront(BasicDelayedAction action)
    {
        QueuedActions.DelayedAction(action.ActionId, action.onStart, queueingType: QueueingType.TO_FRONT);
    }

    public void AddToBack(BasicDelayedAction action)
    {
        QueuedActions.DelayedAction(action.ActionId, action.onStart, queueingType: QueueingType.TO_BACK);
    }

    public void AddStickerToCard(AbstractCard card, AbstractCardSticker stickerToAdd)
    {
        QueuedActions.ImmediateAction(() =>
        {
            card.AddSticker(stickerToAdd);
        });
    }

    public void CheckIsBattleOver()
    {
        QueuedActions.ImmediateAction(() =>
        {
            BattleRules.CheckIsBattleOverAndIfSoSwitchScenes();
        });
    }

    public void PromptPossibleUpgradeOfCard(AbstractCard beforeCard, int? cost = null)
    {
        QueuedActions.ImmediateAction(() =>
        {
            var afterCard = beforeCard.CopyCard();
            afterCard.Upgrade();
            CardModificationDisplayScreen.Instance.Show(
                afterCard: afterCard,
                beforeCard: beforeCard,
                message: "Upgrade card?",
                goldCost: cost
            );
        });
    }


    public void RemoveStatusEffect<T>(AbstractBattleUnit unit) where T: AbstractStatusEffect
    {
        QueuedActions.ImmediateAction(() =>
        {
            unit.StatusEffects.RemoveAll(item => item.GetType() == typeof(T));
        });
    }

    public void KillUnit(AbstractBattleUnit unit)
    {
        QueuedActions.ImmediateAction(() =>
        {
            unit.Die();
        });
    }

    public void ApplyStatusEffect(AbstractBattleUnit unit, AbstractStatusEffect attribute, int stacks = 1)
    {
        QueuedActions.ImmediateAction(() =>
        {
            unit.AddStatusEffect(attribute, stacks);
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


    public void ForceRegenerateIntents(AbstractBattleUnit target)
    { 
        //todo
    }

    public void Taunt(AbstractBattleUnit target, AbstractBattleUnit tauntingCharacter)
    {
        //todo
    }

    public void ReverseTaunt(AbstractBattleUnit target, AbstractBattleUnit tauntingCharacter)
    {
        //todo
    }


    public void ApplyDefense(AbstractBattleUnit target, AbstractBattleUnit source, int baseQuantity)
    {
        QueuedActions.ImmediateAction(() =>
        {
            target.CurrentBlock += BattleRules.GetDefenseApplied(source, target, baseQuantity);
            if (target.CurrentBlock < 0)
            {
                target.CurrentBlock = 0;
            }
        });
    }


    internal void PurgeCardFromDeck(AbstractCard card, QueueingType queueingType = QueueingType.TO_BACK)
    {
        Require.NotNull(card);
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

    internal void AddCardToDrawPile(AbstractCard abstractCard, QueueingType queueingType = QueueingType.TO_BACK)
    {
        Require.NotNull(abstractCard);
        QueuedActions.ImmediateAction(() =>
        {
            ServiceLocator.GetGameStateTracker().Deck.DrawPile.Add(abstractCard);
        }, queueingType);
    }
    internal void AddCardToHand(AbstractCard abstractCard, QueueingType queueingType = QueueingType.TO_BACK)
    {
        Require.NotNull(abstractCard);
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
        gameObject.AddComponent<UiAnimationHandler>();
        animationHandler = this.GetComponent<UiAnimationHandler>();
    }


    public void PromptCardReward(AbstractBattleUnit soldier)
    {
        var soldierClass = soldier.SoldierClass;
        soldier.NumberCardRewardsEligibleFor--;

        var cardsThatCanBeSelected = soldierClass.GetCardRewardChoices();
        QueuedActions.DelayedAction("Choose New Card For Deck", () => {
            CardRewardScreen.Instance.Show(cardsThatCanBeSelected, soldier);
        });
    }


    public void AddCardToPersistentDeck(AbstractCard protoCard, AbstractBattleUnit unit, QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {

            var persistentDeckList = unit.CardsInPersistentDeck;
            if (persistentDeckList.Where(item => item.Id == protoCard.Id).Any())
            {
                throw new Exception("Attempted to add card to deck that already had the same ID as a card in the deck already: " + protoCard.Name);
            }
            unit.AddCardToPersistentDeck(protoCard);
            Debug.Log("Added card to deck: " + protoCard.Name);

            // ServiceLocator.GetCardAnimationManager().RunCreateNewCardAndAddToDiscardPileAnimation(protoCard); //todo
            // Animate: Card created in center of screen, wait for a second, and shrinks while going down to the deck.
            
        }, queueingType);
    }

    public void AttemptPlayCardFromHand(AbstractCard logicalCard, AbstractBattleUnit target, QueueingType queueingType = QueueingType.TO_BACK
        )
    {
        QueuedActions.ImmediateAction(() =>
        {
            if (logicalCard != null)
            {
                if (!logicalCard.CanAfford())
                {
                    Shout(logicalCard.Owner, "I don't have enough energy.");
                }
                else if (logicalCard.CanPlay())
                {
                    logicalCard.PlayCardFromHandIfAble(target);
                }
                else
                {
                    Shout(logicalCard.Owner, "I can't play this!");
                }
            }
            else
            {
                throw new System.Exception("Could not deploy card!  None selected.");
            }
            CheckIsBattleOver();
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

    public void ExhaustCard(AbstractCard protoCard, QueueingType queueingType = QueueingType.TO_BACK)
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



    BattleTurnEndActions turnEndActions = new BattleTurnEndActions();
    public void EndBattleTurn()
    {
        QueuedActions.ImmediateAction(() =>
        {
            turnEndActions.EndTurn();
        });
    }

    public void FleeCombat()
    {
        actionsQueue.Clear();
        GameScenes.SwitchToBattleResultSceneAndProcessCombatResults(CombatResult.FLED);
    }

    public void Shout(AbstractBattleUnit unit, string stuffToSay)
    {
        QueuedActions.ImmediateAction(() =>
        {
            var speechBubbleText = unit.CorrespondingPrefab.SpeechBubbleText;
            var bubbleImg = unit.CorrespondingPrefab.SpeechBubble;
            bubbleImg.gameObject.AddComponent<AppearDisappearImageAnimationPrefab>();
            var appearDisappearPrefab = bubbleImg.gameObject.GetComponent<AppearDisappearImageAnimationPrefab>();
            appearDisappearPrefab.Begin(thingToDoAfterFadingIn: () => { 
                speechBubbleText.gameObject.SetActive(true);
                speechBubbleText.SetText(stuffToSay);
            }, thingToDoBeforeFadingOut:()=>
            {
                speechBubbleText.gameObject.SetActive(false);
            });
        });
    }

    public void PerformAdvanceActionIfPossible(AbstractBattleUnit unit)
    {
        QueuedActions.ImmediateAction(() =>
        {
            if (gameState.energy > 0)
            {
                gameState.energy--;
            }
            else
            {
                EnergyIcon.Instance.Flash();
                Shout(unit, "Not enough energy!");
                return;
            }
            unit.StatusEffects.Add(new AdvancedStatusEffect());
        });
    }
    public void PerformFallbackActionIfPossible(AbstractBattleUnit unit)
    {
        QueuedActions.ImmediateAction(() =>
        {
            if (gameState.energy > 0)
            {
                gameState.energy--;
            }
            else
            {
                EnergyIcon.Instance.Flash();
                Shout(unit, "Not enough energy!");
                return;
            }

            unit.RemoveStatusEffect<AdvancedStatusEffect>();
        });
    }

    public void Advance(AbstractBattleUnit unit)
    {
        QueuedActions.ImmediateAction(() =>
        {
            unit.StatusEffects.Add(new AdvancedStatusEffect());
        });
    }

    public void FallBack(AbstractBattleUnit unit)
    {
        QueuedActions.ImmediateAction(() =>
        {
            if (unit.HasStatusEffect<AdvancedStatusEffect>())
            {
                unit.RemoveStatusEffect<AdvancedStatusEffect>();
            }
        });
    }

    public void OnceFinished(Action action)
    {
        QueuedActions.ImmediateAction(action);
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

    /// <summary>
    /// Spawns a new enemy in battle, if there's room.
    /// </summary>
    /// <param name="unit"></param>
    public void CreateMinionInBattle(AbstractBattleUnit unit)
    {
        var clone = unit.CloneUnit();
        BattleScreenPrefab.INSTANCE.CreateNewEnemyAndRegisterWithGamestate(unit);
    }

    public void HealUnit(AbstractBattleUnit unitHealed, int healedAmount, AbstractBattleUnit healer = null)
    {
        unitHealed.CurrentHp += healedAmount;
        if (unitHealed.CurrentHp > unitHealed.MaxHp)
        {
            unitHealed.CurrentHp = unitHealed.MaxHp;
        }
    }

    public void AttackUnitForDamage(AbstractBattleUnit targetUnit, AbstractBattleUnit sourceUnit, int baseDamageDealt)
    {
        Require.NotNull(targetUnit);
        QueuedActions.DelayedAction("ShakeUnit", () => {
            if (targetUnit.IsDead)
            {
                // do nothing if it's already dead
                IsCurrentActionFinished = true;
                return;
            }
            targetUnit.CorrespondingPrefab.gameObject.AddComponent<ShakePrefab>();
            var shakePrefab = targetUnit.CorrespondingPrefab.gameObject.GetComponent<ShakePrefab>();
            shakePrefab.Begin(() => { IsCurrentActionFinished = true; });

            BattleRules.ProcessDamageWithCalculatedModifiers(sourceUnit, targetUnit, baseDamageDealt);

        });
    }

    public void DamageUnitNonAttack(AbstractBattleUnit targetUnit, AbstractBattleUnit nullableSourceUnit, int baseDamageDealt)
    {
        Require.NotNull(targetUnit);
        QueuedActions.DelayedAction("ShakeUnit", () => {
            if (targetUnit.IsDead)
            {
                // do nothing if it's already dead
                IsCurrentActionFinished = true;
                return;
            }
            targetUnit.CorrespondingPrefab.gameObject.AddComponent<ShakePrefab>();
            var shakePrefab = targetUnit.CorrespondingPrefab.gameObject.GetComponent<ShakePrefab>();
            shakePrefab.Begin(() => { IsCurrentActionFinished = true; });

            BattleRules.ProcessDamageWithCalculatedModifiers(nullableSourceUnit, targetUnit, baseDamageDealt);

        });
    }

    public void DestroyUnit(AbstractBattleUnit unit)
    {
        Require.NotNull(unit);
        QueuedActions.ImmediateAction(() =>
        {
            // todo: Figure out approrpiate despawning logic.
            unit.CorrespondingPrefab.HideUnit();
        });
    }

    public void ChangeUnit(AbstractBattleUnit unit, Action<AbstractBattleUnit> action)
    {
        Require.NotNull(unit);
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