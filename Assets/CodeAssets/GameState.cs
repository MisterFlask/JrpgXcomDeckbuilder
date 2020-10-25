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
        totalDeck.ForEach(item => deck.AddNewCardToDeck(item));

    }

    public int BattleTurn { get; set; }
    public BattleDeck Deck { get; set; } = new BattleDeck();

    Card cardSelected = null;

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

    #region UI State
    public AbstractBattleUnit CharacterSelected { get; set; }
    #endregion
}
