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
    public void Start()
    {
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

    public Text coinResourceText;
    public Text energyResourceText;

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

    [Obsolete("Use Actionmanager instead")]
    public void DeployCardSelectedIfApplicable(Card card)
    {
        var cardSelected = card;
        if (cardSelected != null)
        {
            var behavior = cardSelected.GetComponent<PlayerCard>().LogicalCard;
            if (behavior.CanPlay())
            {
                behavior.PlayCardFromHandIfAble(null);
            }
        }
        else
        {
            throw new System.Exception("Could not deploy card!  None selected.");
        }
    }
    #endregion

    #region resources


    public int coins { get; private set; } = 0;
    public int energy { get; set; } = 3;
    public int maxEnergy { get; set; } = 3;



    public void ModifyCoin(int mod)
    {
        coins += mod;
        UpdateResources();
    }

    #endregion

    public void UpdateResources()
    {
        this.coinResourceText.text = coins.ToString();
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
