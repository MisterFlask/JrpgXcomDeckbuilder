using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static TacMapController;
using HyperCard;
using UnityEngine.UI;
using System;
using System.Linq;

public class GameState : MonoBehaviour
{

    public static GameState Instance;
    public void Start()
    {
        Instance = this;
        var totalDeck = new List<AbstractCard>
        {
        }; //Temporary

        this.gameObject.AddComponent<TacMapController>();
        UpdateResources();

        var deck = Deck;
        totalDeck.ForEach(item => deck.AddNewCardToDeck(item));

        ScriptExecutionTracker.ScriptsFinishedExecuting.Add(nameof(GameState));
    }
    private List<Mission> Missions { get; set; } = new List<Mission>();

    public TMPro.TextMeshProUGUI coinResourceText;
    public TMPro.TextMeshProUGUI energyResourceText;

    public int BattleTurn { get; set; }
    public Deck Deck { get; set; } = new Deck();

    #region ui_handling

    Card cardSelected = null;

    public void RemoveMission(Mission mission)
    {
        Missions.Remove(mission);
    }

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

    #endregion

    #region resources


    public int money { get; set; } = 0;
    public int energy { get; set; } = 3;
    public int maxEnergy { get; set; } = 3;



    public void ModifyCoin(int mod)
    {
        money += mod;
        UpdateResources();
    }

    #endregion

    public void UpdateResources()
    {
        this.coinResourceText.text = money.ToString();
        this.energyResourceText.text = $"{energy}/{maxEnergy}";
    }

    public void AddMission (Mission mission)
    {
        Missions.Add(mission);
    }

    public IEnumerable<Mission> GetMissions()
    {
        return Missions;
    }

    public void Update()
    {
        this.UpdateResources();
    }

    #region BATTLE SCREEN

    public List<AbstractBattleUnit> PersistentCharacterRoster { get; set; } = new List<AbstractBattleUnit>();
    public List<AbstractBattleUnit> AllyUnitsInBattle { get; set; } = new List<AbstractBattleUnit>();
    public List<AbstractBattleUnit> EnemyUnitsInBattle { get; set; } = new List<AbstractBattleUnit>();

    #endregion
}
