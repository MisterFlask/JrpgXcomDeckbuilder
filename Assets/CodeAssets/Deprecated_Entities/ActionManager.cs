using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;
using HyperCard;

public class ActionManager : MonoBehaviour
{
    FogOfWarManager fogManager = new FogOfWarManager();
    private Deck deck => ServiceLocator.GetGameStateTracker().Deck;

    GameState gameState;

    public Image ArmageddonCounterImage;

    // Used to end the current action/start the next delayed action
    public bool IsCurrentActionFinished { get; set; }

    public List<DelayedAction> actionsQueue = new List<DelayedAction>();

    public RivalUnitPrefab RivalUnitPrefab => ServiceLocator.GetRivalUnitPrefab();

    public void AddPassiveEffect(PassiveEffect effect)
    {
        QueuedActions.ImmediateAction(() =>
        {
            var newEffect = effect.ClonePassiveEffect<PassiveEffect>();
            ServiceLocator.GetGameStateTracker().AddPassiveEffect(newEffect);
        });
    }

    public void PromptPossibleUpgradeOfCard(AbstractCard beforeCard)
    {
        QueuedActions.ImmediateAction(() =>
        {
            var afterCard = beforeCard.CopyCard();
            afterCard.Upgrade();
            var productionCost = beforeCard.ProductionCostForNextUpgrade();
            ServiceLocator.GetUiStateManager().SwitchToCardModificationScreen(new ShowingCardModificationMessage
            {
                afterCard = afterCard,
                beforeCard = beforeCard,
                CanCancel = true,
                ThingToDoUponConfirmation = () =>
                {
                    beforeCard.Upgrade(); 
                    ServiceLocator.GetCardAnimationManager().RefreshCardAppearance(beforeCard);
                    this.ModifyCommerce(-1 * productionCost);
                },
                Name = "Modify card? Will cost you " + productionCost + $" production",
                TitleMessage = "Modify card?  Will cost you " + productionCost + " production."
            });
        });
    }
    public void PromptPossibleTransformationOfCard(AbstractCard beforeCard)
    {
        QueuedActions.ImmediateAction(() =>
        {
            var afterCard = beforeCard.GetTransformedCardOnProductionAction();
            var productionCost = beforeCard.ProductionCostForNextUpgrade();
            ServiceLocator.GetUiStateManager().SwitchToCardModificationScreen(new ShowingCardModificationMessage
            {
                afterCard= afterCard,
                beforeCard= beforeCard,
                CanCancel = true,
                ThingToDoUponConfirmation = () =>
                {
                    this.PurgeCardFromDeck(beforeCard);
                    this.AddCardToDeck(afterCard);
                    this.ModifyCommerce(-1 * productionCost);
                },
                Name = "Modify card? Will cost you " + productionCost + $" production",
                TitleMessage = "Modify card?  Will cost you " + productionCost + " production."
             });
        });
    }

    public void ModifyPassiveEffect(PassiveEffect effect, Action<PassiveEffect> action)
    {
        QueuedActions.ImmediateAction(() =>
        {
            action.Invoke(effect);
            AllEffectsPanel.RefreshPassiveEffectsDisplay();
        });
    }

    public void ModifyStacks(PassiveEffect effect, int stacks)
    {
        QueuedActions.ImmediateAction(() =>
        {
            effect.Stacks += stacks;
            AllEffectsPanel.RefreshPassiveEffectsDisplay();
        });
    }

    public void RemovePassiveEffect(PassiveEffect effect)
    {

        QueuedActions.ImmediateAction(() =>
        {
            ServiceLocator.GetGameStateTracker().RemovePassiveEffects((item) => item.Id == effect.Id);
        });
    }

    public void UpgradeCard(AbstractCard card)
    {
        QueuedActions.ImmediateAction(() =>
        {
            card.Upgrade();
        });

    }

    public void PerformProductionActionOnCardSelectedIfPossible()
    {
        QueuedActions.ImmediateAction(() =>
        {

            var cardSelected = ServiceLocator.GetGameStateTracker().GetCardSelected();
            if (cardSelected == null)
            {
                return;
            }
            if (cardSelected.LogicalCard.GetApplicableProductionAction() == ProductionAction.TRANSFORM_CARD)
            {
                PromptPossibleTransformationOfCard(cardSelected.LogicalCard);
            }

            if (cardSelected.LogicalCard.GetApplicableProductionAction() == ProductionAction.UPGRADE_CARD)
            {
                PromptPossibleUpgradeOfCard(cardSelected.LogicalCard);
            }
            if (cardSelected.LogicalCard.GetApplicableProductionAction() == ProductionAction.NONE_ALLOWED)
            {
                return;
            }
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


    public void PlayCardForCost(Card card, Action invocation, QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {

            card.LogicalCard.PayCostsForCard();
            invocation.Invoke();
            ServiceLocator.GetCardAnimationManager().MoveCardToDiscardPile(card.LogicalCard);
            PerformStateBasedActions();
        }, queueingType);
    }

    public void DeployCardSelectedIfApplicable(Card card, TileLocation tileLocationSelected = null, QueueingType queueingType = QueueingType.TO_BACK
        )
    {
        QueuedActions.ImmediateAction(() =>
        {

#pragma warning disable CS0618 // Type or member is obsolete
            gameState.DeployCardSelectedIfApplicable(card, tileLocationSelected);
#pragma warning restore CS0618 // Type or member is obsolete
            PerformStateBasedActions();
        }, queueingType);
    }

    public void ResolveMissionSuccess(Mission mission)
    {

        QueuedActions.ImmediateAction(() =>
        {

            PerformStateBasedActions();
        });
    }

    public void ResolveMissionFailure(Mission mission)
    {
        QueuedActions.ImmediateAction(() =>
        {

            mission.FailMission();
            PerformStateBasedActions();
        });

    }
    /// <summary>
    /// meant to be idempotent
    /// </summary>
    public void PerformStateBasedActions()
    {
        QueuedActions.ImmediateAction(() =>
        {

            var missionsToBeCompleted = gameState.GetMissions().Where(item => item.IsSuccessful()).ToList();
            if (missionsToBeCompleted.Count() > 0)
            {
                foreach (var item in missionsToBeCompleted)
                {
                    item.OnMissionSuccess();
                    gameState.RemoveMission(item);
                }
            }

            var missionsToBeFailed = gameState.GetMissions().Where(item => item.IsFailed()).ToList();
            if (missionsToBeFailed.Count() > 0)
            {
                foreach (var item in missionsToBeFailed)
                {
                    item.OnMissionFailure();
                    gameState.RemoveMission(item);
                }
            }

            RefreshUi();
        });
    }
    public void RefreshUi()
    {
        QueuedActions.ImmediateAction(() =>
        {

            var missionContentPanel = GameObject.FindObjectOfType<AllMissionsContentPanel>();
            missionContentPanel.Refresh();
        });
    }

    public void DiscardCard(AbstractCard protoCard,  QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {

            gameState.Deck.MoveCardToPile(protoCard, CardPosition.DISCARD);
            ServiceLocator.GetCardAnimationManager().MoveCardToDiscardPile(protoCard);
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

    public void PromptPlayerWithDilemma(Dilemma dilemma)
    {
        QueuedActions.DelayedAction("dilemma", () =>
        {
            ServiceLocator.GetUiStateManager().ShowDilemmaToPlayer(dilemma);
        });
    }

    internal void ModifyEnergy(int v, ModifyType type = ModifyType.SET, QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {

            if (v == 0)
            {
                return;
            }
            gameState.modifyEnergy(v, type);
        }, queueingType);
    }


    public void ModifyArmageddonCounter(int amount, QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {

            if (amount == 0)
            {
                return;
            }
            gameState.modifyArmageddonCounter(amount);
            animationHandler.PulseAndFlashElement(ArmageddonCounterImage);
        }, queueingType);
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
    public void ModifyStability(int amount, QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {

            if (amount == 0)
            {
                return;
            }
            gameState.modifyStability(amount);
            animationHandler.PulseAndFlashElement(StabilityImage);
        }, queueingType);
    }

    private float FACTION_HIGHLIGHT_ALPHA = .5f;

    public void AssignStartingTilesAndUnitsForEachFaction()
    {
        var tilesByLocation = ServiceLocator.GetTileMap().TilesByLocation;
        var tileLocations = tilesByLocation.Keys;
        var factions = ServiceLocator.GetGameStateTracker().Factions;

        var playerOwnedTiles = GetStartingPlayerOwnedTiles();

        foreach(var tile in playerOwnedTiles)
        {
            ReassignOwnershipOfTile(tile, Faction.PlayerFaction);
        }

        // declare faction "bases"
        foreach (var faction in factions)
        {
            faction.FactionBase = tileLocations.PickRandomWhere(item => item.NotIn(playerOwnedTiles));
            CreateUnitAtTile(new DefaultRivalUnit
            {
            }, faction.FactionBase, faction);
        }

        // now, declare that tiles belong to whichever faction's base is closest.
        foreach (var tile in tileLocations.Where(item => item.NotIn(playerOwnedTiles)))
        {
            int maxDistanceToBestFaction = 10000; // arbitrary big number
            var currentBestFaction = factions.First();
            foreach(var faction in factions)
            {
                var distanceToBase = faction.FactionBase.DistanceToTile(tile);
                if (distanceToBase < maxDistanceToBestFaction)
                {
                    maxDistanceToBestFaction = distanceToBase;
                    currentBestFaction = faction;
                }
            }
            ReassignOwnershipOfTile(tile, currentBestFaction);
        }
        
        // first, allocate four tiles to the player.
    }

    #region tilemap allocation impl


    private List<TileLocation> GetStartingPlayerOwnedTiles()
    {
        var centerOfMap = ServiceLocator.GetTileMap().GetCenterOfMap();
        var randomNeighbors = centerOfMap.GetNeighbors().PickRandom(3);
        var ret = new List<TileLocation>();
        ret.AddRange(randomNeighbors);
        ret.Add(centerOfMap);
        return ret;
    }
    #endregion


    public void ReassignOwnershipOfTile(TileLocation tileLocation, Faction newOwner, QueueingType queueingType = QueueingType.TO_BACK)
    {
        QueuedActions.ImmediateAction(() =>
        {

            Debug.Log($"Attempting to reassign {tileLocation} to {newOwner.Name}");
            var tile = tileLocation.ToTile();
            if (tile.Owner == newOwner)
            {

                return;
            }
            // regardless, switch color
            // special casing Wilderness

            if (newOwner == Faction.WildernessFaction)
            {
                // tile.HexPrefab.BlurryWhiteOutline.enabled = false;
            }

            if (newOwner.IsPlayer)
            {
                ServiceLocator.GetCamera().ZoomToLookAt(tile.HexPrefab.gameObject);
            }
            tile.Owner = newOwner;

            fogManager.UpdateFogOfWar();
            tile.HexPrefab.Refresh();
            tile.GetNeighbors().ForEach(item => item.HexPrefab.Refresh());
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


    TurnEndActions turnEndActions = new TurnEndActions();
    public void EndTurn()
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

    #region Purely Graphical Effects
    public List<TileLocation> TilesHighlighted = new List<TileLocation>();


    public void HighlightTiles(List<TileLocation> tiles)
    {
        TilesHighlighted.AddRange(tiles);
        var logicalTiles = tiles.Select(item => item.ToTile());
        foreach(var logicalTile in logicalTiles)
        {
            // logicalTile.HexPrefab.BlurryWhiteOutline.color = logicalTile.FuzzyOutlineProperties.BaseColor.WithAlpha(1.0f);
        }
    }

    public void ClearHighlightingOnTiles()
    {
        // TilesHighlighted.ForEach(item => item.ToTile().HexPrefab.BlurryWhiteOutline.color = item.ToTile().FuzzyOutlineProperties.BaseColor);
        TilesHighlighted.Clear();
    }
    #endregion

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


    public void CreateUnitAtTile(AbstractRivalUnit unit, TileLocation tile, Faction faction)
    {
        var copyOfUnit = unit.Clone();
        var unitPrefab = ServiceLocator.GetRivalUnitPrefab().Spawn(ServiceLocator.GetUnitFolder());
        unitPrefab.RivalUnitEntity = copyOfUnit;
        copyOfUnit.Prefab = unitPrefab;
        copyOfUnit.Faction = faction;
        copyOfUnit.TileLocation = tile;
        unitPrefab.transform.position = tile.ToTile().HexPrefab.transform.position;
        gameState.RivalUnits.Add(copyOfUnit);
    }

    public void MoveUnitAttempt(AbstractRivalUnit unit, TileLocation tileLocation)
    {
        QueuedActions.ImmediateAction(() => 
        { 

            if (tileLocation.ToTile().Owner == Faction.PlayerFaction)
            {
                EnactRivalUnitToTileCombat(unit, tileLocation);
            }
            else
            {
                MoveUnitPrefab(unit, tileLocation);
            }
        });
    }

    private void EnactRivalUnitToTileCombat(AbstractRivalUnit unit, TileLocation tileLocation)
    {
        QueuedActions.ImmediateAction(() =>
        {
            DamageTileFromUnit(unit, tileLocation, unit.Power);
            DamageUnit(unit, tileLocation.ToTile().Power);
        });
    }

    public void DamageTileFromUnit(AbstractRivalUnit unit, TileLocation location, int damage)
    {
        QueuedActions.ImmediateAction(() =>
        {
            location.ToTile().Toughness -= unit.Power;
            if (location.ToTile().Toughness <= 0)
            {
                // It's now another faction's
                ReassignOwnershipOfTile(location, unit.Faction);
            }
        });
    }

    public void DamageUnit(AbstractRivalUnit unit, int damage)
    {
        unit.Toughness -= damage;
        if (unit.Toughness <= 0)
        {
            DestroyUnit(unit);
        }

    }

    public void DestroyUnit(AbstractRivalUnit unit)
    {
        QueuedActions.ImmediateAction(() =>
        {
            ServiceLocator.GetGameStateTracker().RivalUnits.Remove(unit);
            unit.Prefab.gameObject.Despawn();
        });
    }

    private void MoveUnitPrefab(AbstractRivalUnit unit, TileLocation tileLocation)
    {


        QueuedActions.DelayedAction("UnitMovement", () =>
        {
            unit.TileLocation = tileLocation;
            StartCoroutine(MoveFromTo(unit.Prefab.transform, unit.Prefab.transform.position, tileLocation.GetWorldCoordinatesOfTile(), 1.0f));
        });
    }

    public void ChangeUnit(AbstractRivalUnit unit, Action<AbstractRivalUnit> action)
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