using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using Assets.CodeAssets.UI.Screens.BattleScreen;
using Assets.CodeAssets.Utils;

public class BattleScreenPrefab : MonoBehaviour
{
    public static BattleScreenPrefab INSTANCE;
    public BattleScreenPrefab()
    {
    }

    public void Awake()
    {
        EagerMonobehaviour.InitializeAllEagerMonobehaviours();
        INSTANCE = this;
    }

    private GameState state => ServiceLocator.GameState();
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
        if (GameState.Instance.CurrentMission?.BattleBackground != null)
        {
            Image.sprite = GameState.Instance.CurrentMission.BattleBackground.ToSprite();
        }

        if (StartingAllies.Count > PotentialBattleEntityAllySpots.Count)
        {
            throw new System.Exception("Too many allies for available number of spots");
        }
        if (StartingEnemies.Count > PotentialBattleEntityEnemySpots.Count)
        {
            throw new System.Exception("Too many enemies for available number of spots");
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

    public BattleUnitPrefab GetFirstEmptyBattleUnitHolder()
    {
        return PotentialBattleEntityEnemySpots.FirstOrDefault(item => item.UnderlyingEntity == null);
    }

    public bool IsRoomForAnotherEnemy()
    {
        return GetFirstEmptyBattleUnitHolder() != null;
    }

    public CreateEnemyResult CreateNewEnemyAndRegisterWithGamestate(AbstractBattleUnit battleUnit)
    {
        var firstEmptyBattleUnitHolder = GetFirstEmptyBattleUnitHolder();
        if (firstEmptyBattleUnitHolder == null)
        {

            return new CreateEnemyResult
            {
                FailedDueToNoSpaceLeft = true
            };
        }

        firstEmptyBattleUnitHolder.Initialize(battleUnit);
        GameState.Instance.EnemyUnitsInBattle.Add(battleUnit);
        return new CreateEnemyResult();
    }


    public void Start()
    {
        INSTANCE = this;
        SelectCardInHandOverlay.Hide();
        ShowDeckScreen.Hide();

        GameObject.FindObjectOfType<UtilityObjectHolder>().Start();

        PotentialBattleEntityEnemySpots = EnemyUnitSpotsParent.GetComponentsInChildren<BattleUnitPrefab>().ToList();
        PotentialBattleEntityAllySpots = AllyUnitSpotsParent.GetComponentsInChildren<BattleUnitPrefab>().ToList();

        /// TODO:  Remove after getting strategic map up and running

        state.Deck = new BattleDeck();
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
        Setup(ServiceLocator.GameState().EnemyUnitsInBattle, ServiceLocator.GameState().AllyUnitsInBattle);

        PotentialBattleEntityAllySpots.ForEach(item => item.HideOrShowAsAppropriate());
        PotentialBattleEntityEnemySpots.ForEach(item => item.HideOrShowAsAppropriate());

        state.EnemyUnitsInBattle.ForEach(item => item.InitForBattle());
        state.AllyUnitsInBattle.ForEach(item => item.InitForBattle());


        BattleStarter.StartBattle(this);
    }

    public void Update()
    {
    }
}

public class CreateEnemyResult
{
    public bool FailedDueToNoSpaceLeft = false;
    public bool Successful => !FailedDueToNoSpaceLeft;
}
