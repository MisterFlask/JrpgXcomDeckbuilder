using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class BattleScreenPrefab : MonoBehaviour
{
    public static BattleScreenPrefab INSTANCE;
    public BattleScreenPrefab()
    {
        INSTANCE = this;
    }

    private GameState state => ServiceLocator.GetGameStateTracker();
    private ActionManager action => ServiceLocator.GetActionManager();

    private List<BattleUnitPrefab> PotentialBattleEntityEnemySpots;
    private List<BattleUnitPrefab> PotentialBattleEntityAllySpots;

    public GameObject EnemyUnitSpotsParent;
    public GameObject AllyUnitSpotsParent;

    public Image Image;

    /// <summary>
    /// These next couple attributes are just for tracking what we're currently mousing over.
    /// </summary>
    public static AbstractIntent IntentMousedOver { get; set; }
    public static AbstractBattleUnit BattleUnitMousedOver { get; set; }
    public static AbstractCard CardMousedOver { get; set; }

    public void Setup(List<AbstractBattleUnit> StartingEnemies, List<AbstractBattleUnit> StartingAllies
        )
    {
        Image.sprite = GameState.Instance.CurrentMission.BattleBackground.ToSprite();
        if (StartingAllies.Count > PotentialBattleEntityAllySpots.Count)
        {
            throw new System.Exception("Too many allies for availabel number of spots");
        }
        if (StartingEnemies.Count > PotentialBattleEntityEnemySpots.Count)
        {
            throw new System.Exception("Too many enemies for availabel number of spots");
        }
        PotentialBattleEntityEnemySpots = PotentialBattleEntityEnemySpots.Shuffle().ToList();
        PotentialBattleEntityAllySpots = PotentialBattleEntityAllySpots.Shuffle().ToList();
        for(int i = 0; i < StartingAllies.Count; i++)
        {
            PotentialBattleEntityAllySpots[i].Initialize(StartingAllies[i]);
        }
        for (int i = 0; i < StartingEnemies.Count; i++)
        {
            PotentialBattleEntityEnemySpots[i].Initialize(StartingEnemies[i]);
        }
    }

    public void Start()
    {
        GameObject.FindObjectOfType<UtilityObjectHolder>().Start();

        PotentialBattleEntityEnemySpots = EnemyUnitSpotsParent.GetComponentsInChildren<BattleUnitPrefab>().ToList();
        PotentialBattleEntityAllySpots = AllyUnitSpotsParent.GetComponentsInChildren<BattleUnitPrefab>().ToList();

        /// TODO:  Remove after getting strategic map up and running


        foreach (var character in state.AllyUnitsInBattle)
        {
            character.InitForBattle();
            foreach (var card in character.BattleDeck)
            {
                state.Deck.AddNewCardToDiscardPile(card.CopyCard(logicallyIdenticalToExistingCard: true));
            }
        }

        action.DrawCards(5);

        /// END TODO
        Setup(ServiceLocator.GetGameStateTracker().EnemyUnitsInBattle, ServiceLocator.GetGameStateTracker().AllyUnitsInBattle);

        PotentialBattleEntityAllySpots.ForEach(item => item.HideOrShowAsAppropriate());
        PotentialBattleEntityEnemySpots.ForEach(item => item.HideOrShowAsAppropriate());

        state.EnemyUnitsInBattle.ForEach(item => item.InitForBattle());
        state.AllyUnitsInBattle.ForEach(item => item.InitForBattle());

        BattleStarter.StartBattle(this);
    }
}
