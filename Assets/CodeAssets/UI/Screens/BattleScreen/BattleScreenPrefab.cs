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
    private List<BattleUnitPrefab> PotentialBattleEntityLargeEnemySpots;
    private List<BattleUnitPrefab> PotentialBattleEntityHugeEnemySpots;
    private List<BattleUnitPrefab> PotentialBattleEntityAllySpots;

    public GameObject EnemyUnitSpotsParent;
    public GameObject LargeEnemyUnitSpotsParent;
    public GameObject HugeEnemyUnitSpotsParent;
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
        var smallEnemies = StartingEnemies.Where(item => item.UnitSize == UnitSize.SMALL).ToList();
        var mediumEnemies = StartingEnemies.Where(item => item.UnitSize == UnitSize.MEDIUM).ToList();
        var largeEnemies = StartingEnemies.Where(item => item.UnitSize == UnitSize.LARGE).ToList();

        var forbiddenSmallEnemyIndices = new List<int>();
        var forbiddenMediumEnemyIndices = new List<int>();
        if (mediumEnemies.Count() >= 1)
        {
            forbiddenSmallEnemyIndices.Add(0);
            forbiddenSmallEnemyIndices.Add(1);
            forbiddenSmallEnemyIndices.Add(3);
            forbiddenSmallEnemyIndices.Add(4);
        }
        if (mediumEnemies.Count() == 2)
        {
            forbiddenSmallEnemyIndices.Add(6);
            forbiddenSmallEnemyIndices.Add(7);
            forbiddenSmallEnemyIndices.Add(9);
            forbiddenSmallEnemyIndices.Add(10);
        }
        if (largeEnemies.Count() == 1)
        {
            forbiddenSmallEnemyIndices.Add(0);
            forbiddenSmallEnemyIndices.Add(1);
            forbiddenSmallEnemyIndices.Add(2);
            forbiddenSmallEnemyIndices.Add(3);
            forbiddenSmallEnemyIndices.Add(4);
            forbiddenSmallEnemyIndices.Add(5);
            forbiddenMediumEnemyIndices.Add(0);
        }

        if (largeEnemies.Count() > 1)
        {
            throw new System.Exception("Too many large enemies to fit in combat");
        }
        if (mediumEnemies.Count() > 2)
        {
            throw new System.Exception("Too many medium enemies to fit in combat");
        }
        if (smallEnemies.Count() > 12)
        {
            throw new System.Exception("Too many small enemies to fit in combat");
        }

        var acceptableStartingSmallEnemySpots = PotentialBattleEntityEnemySpots
            .Where(item => !forbiddenSmallEnemyIndices.Contains(PotentialBattleEntityEnemySpots.IndexOf(item)))
            .ToList();


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

        for(int i = 0; i < StartingAllies.Count; i++)
        {
            PotentialBattleEntityAllySpots[i].Initialize(StartingAllies[i]);
        }
        for (int i = 0; i < smallEnemies.Count(); i++)
        {
            acceptableStartingSmallEnemySpots[i].Initialize(smallEnemies[i]);
        }
        for (int i = 0; i < mediumEnemies.Count(); i++)
        {
            PotentialBattleEntityLargeEnemySpots[i].Initialize(mediumEnemies[i]);
        }
        for (int i = 0; i < largeEnemies.Count(); i++)
        {
            PotentialBattleEntityHugeEnemySpots[i].Initialize(largeEnemies[i]);
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
        PotentialBattleEntityLargeEnemySpots = LargeEnemyUnitSpotsParent.GetComponentsInChildren<BattleUnitPrefab>().ToList();
        PotentialBattleEntityHugeEnemySpots = HugeEnemyUnitSpotsParent.GetComponentsInChildren<BattleUnitPrefab>().ToList();

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
        PotentialBattleEntityLargeEnemySpots.ForEach(item => item.HideOrShowAsAppropriate());
        PotentialBattleEntityHugeEnemySpots.ForEach(item => item.HideOrShowAsAppropriate());

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
