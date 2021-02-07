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

    public List<AbstractAugmentation> AugmentationInventory { get; set; } = new List<AbstractAugmentation>
    {
        new PerkAugmentation(new AgilePerk())
    };

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

    public int money { get; set; } = 0;
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

    #region UI State
    public AbstractBattleUnit CharacterSelected { get; set; }
    public int Day { get; set; }
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
