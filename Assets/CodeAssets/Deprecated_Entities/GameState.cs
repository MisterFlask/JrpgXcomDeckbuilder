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
    public List<AbstractRivalUnit> RivalUnits { get; } = new List<AbstractRivalUnit>();
    
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
    public List<Faction> Factions;


    public Text scienceResourceText;

    public Text coinResourceText;
    public Text armageddonCounterText;

    public Text stabilityResourceText;

    public Text discardResourceText;
    public Text drawPileResourceText;

    public Text energyResourceText;

    public int Turn { get; set; }
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
    public void DeployCardSelectedIfApplicable(Card card, TileLocation tileLocationSelected = null)
    {
        var cardSelected = card;
        var cardWasDeployedWithoutLocation = tileLocationSelected == null;
        if (cardSelected != null)
        {
            var behavior = cardSelected.GetComponent<PlayerCard>().LogicalCard;
            if (cardWasDeployedWithoutLocation && behavior.CanPlay())
            {
                behavior.PlayCard();
                if (Deck.Hand.Contains(behavior))
                {
                    Deck.MoveCardToPile(behavior, CardPosition.DISCARD);
                    ServiceLocator.GetCardAnimationManager().MoveCardToDiscardPile(behavior);
                }
            }
            if (!cardWasDeployedWithoutLocation && behavior.CanDeployToRegion(tileLocationSelected))
            {
                behavior.DeployToRegion(tileLocationSelected);
                if (Deck.Hand.Contains(behavior))
                {
                    Deck.MoveCardToPile(behavior, CardPosition.DISCARD);
                    ServiceLocator.GetCardAnimationManager().MoveCardToDiscardPile(behavior);
                }
            }
        }
        else
        {
            throw new System.Exception("Could not deploy card!  None selected.");
        }
    }
    #endregion

    #region regions
    #endregion

    #region resources


    public int coins { get; private set; } = 0;
    public int armageddonCounter { get; private set; } = 0;
    public int stability { get; private set; } = 10;

    public int energy { get; private set; } = 3;
    public int maxEnergy { get; private set; } = 3;

    public void modifyStability(int mod)
    {
        stability += mod;
        UpdateResources();
    }
    public void modifyArmageddonCounter(int mod)
    {
        armageddonCounter += mod;
        UpdateResources();
    }

    public void modifyEnergy(int mod, ModifyType type)
    {
        if (type == ModifyType.ADD_VALUE)
        {
            energy += mod;
        }
        else
        {
            energy = mod;
        }
        UpdateResources();
    }

    public void ModifyCoin(int mod)
    {
        coins += mod;
        UpdateResources();
    }

    #endregion

    public void UpdateResources()
    {
        this.coinResourceText.text = coins.ToString();
        this.stabilityResourceText.text = stability.ToString();
        this.armageddonCounterText.text = armageddonCounter.ToString();

        this.discardResourceText.text = Deck.DiscardPile.Count.ToString();
        this.drawPileResourceText.text = Deck.DrawPile.Count.ToString();
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
}
