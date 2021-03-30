using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HyperCard;
using UnityEngine.UI;
using System;
using System.Linq;

public class GameState
{
    public GameState()
    {
        Initialize();
    }

    public static GameState Instance = new GameState();
    public void Initialize()
    {
        Instance = this;
        var totalDeck = new List<AbstractCard>
        {
        };
        var deck = Deck;
        totalDeck.ForEach(item => deck.AddNewCardToDiscardPile(item));

    }

    /// <summary>
    /// Battles start at turn ZERO, and iterate at the start of each new turn.
    /// </summary>
    public int BattleTurn { get; set; } = 0;
    public BattleDeck Deck { get; set; } = new BattleDeck();

    Card cardSelected = null;

    public List<AbstractCard> CardInventory { get; set; } = new List<AbstractCard>
    {
        new Bayonet()
    };

    public List<SoldierPerk> AugmentationInventory { get; set; } = new List<SoldierPerk>
    {
        new AgilePerk()
    };


    public List<AbstractStatusEffect> GlobalBattleStatusEffects = new List<AbstractStatusEffect>();

    public Card GetCardSelected()
    {
        return cardSelected;
    }

    public void SetCardSelected(Card card)
    {
        this.cardSelected = card;
    }

    public void UnselectCard()
    {
        this.cardSelected = null;
    }

    public int Credits { get; set; } = 0;
    public int energy { get; set; } = 3;
    public int maxEnergy { get; set; } = 3;
    public List<AbstractBattleUnit> PersistentCharacterRoster { get; set; } = new List<AbstractBattleUnit>();
    public List<AbstractBattleUnit> AllyUnitsInBattle { get; set; } = new List<AbstractBattleUnit>();
    public List<AbstractBattleUnit> EnemyUnitsInBattle { get; set; } = new List<AbstractBattleUnit>();
    public Mission CurrentMission { get; set; }

    /// <summary>
    /// List of global battle mechanics for the mission.  Note that this list is emptied at the start of each mission.
    /// </summary>
    public List<AbstractGlobalBattleMechanic> GlobalBattleMechanics = new List<AbstractGlobalBattleMechanic>();

    public ShopData ShopData { get; } = new ShopData();

    public int cardsPlayedThisTurn = 0;

    #region UI State
    public AbstractBattleUnit CharacterSelected { get; set; }
    public int Day { get; set; }
    #endregion

    #region Convenience
    public List<AbstractBattleUnit> GetUnitsAttackingUnit(AbstractBattleUnit target)
    {
        var enemiesAttackingTarget = EnemyUnitsInBattle.Where(enemy => enemy.CurrentIntents.Any(intent => IsIntentAttackingMe(intent, target)));
        return enemiesAttackingTarget.ToList();
    }

    private bool IsIntentAttackingMe(AbstractIntent intent, AbstractBattleUnit unitBeingAttacked)
    {
        if (intent is SingleUnitAttackIntent)
        {
            var singleUnitAttack = intent as SingleUnitAttackIntent;
            if (singleUnitAttack.Target == unitBeingAttacked)
            {
                return true;
            }
        }
        return false;
    }

    #endregion
}

/// <summary>
/// These represent triggers/action hooks that aren't associated to any specific character in battle.
/// Examples: Retreating mechanic, most mission modifiers
/// </summary>
public abstract class AbstractGlobalBattleMechanic
{
    public int Stacks { get; set; }

    public virtual void OnTurnStart()
    {

    }

    public virtual void OnCardPlayed() 
    { 
    
    }
}