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


    private List<int> ForbiddenSmallEnemyIndicesIfLargeEnemyExists = new List<int>
    {
        0,1,2,3,4,5
    };

    private List<int> ForbiddenSmallEnemyIndicesIfMediumEnemyExistsInSlotOne = new List<int>
    {
        0,1,3,4
    };

    private List<int> ForbiddenSmallEnemyIndicesIfMediumEnemyExistsInSlotTwo = new List<int>
    {
        6,7,9,10
    };

    
    public List<int> GetForbiddenIndicesForSmallCharacters_BasedOnExistingPrefabPopulations()
    {
        var forbiddenSmallSlots = new List<int>();

        var mediumEnemyInSlotOne = PotentialBattleEntityLargeEnemySpots[0].UnderlyingEntity != null;
        var mediumEnemyInSlotTwo = PotentialBattleEntityLargeEnemySpots[1].UnderlyingEntity != null;
        var largeEnemyExists = PotentialBattleEntityHugeEnemySpots[0].UnderlyingEntity != null;

        if (mediumEnemyInSlotOne)
        {
            forbiddenSmallSlots.AddRange(ForbiddenSmallEnemyIndicesIfMediumEnemyExistsInSlotOne);
        }
        if (mediumEnemyInSlotTwo)
        {
            forbiddenSmallSlots.AddRange(ForbiddenSmallEnemyIndicesIfMediumEnemyExistsInSlotTwo);
        }
        if (largeEnemyExists)
        {
            forbiddenSmallSlots.AddRange(ForbiddenSmallEnemyIndicesIfLargeEnemyExists);
        }

        return forbiddenSmallSlots;
    }

    public List<BattleUnitPrefab> GetAvailableSpotsForNewSmallUnits()
    {
        var emptySpots = PotentialBattleEntityEnemySpots.Where(item => item.UnderlyingEntity == null);
        var forbiddenIndices = GetForbiddenIndicesForSmallCharacters_BasedOnExistingPrefabPopulations();

        return emptySpots
            .Where(item => !forbiddenIndices.Contains(PotentialBattleEntityEnemySpots.IndexOf(item)))
            .ToList();
    }

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
            forbiddenSmallEnemyIndices.AddRange(ForbiddenSmallEnemyIndicesIfMediumEnemyExistsInSlotOne);
        }
        if (mediumEnemies.Count() == 2)
        {
            forbiddenSmallEnemyIndices.AddRange(ForbiddenSmallEnemyIndicesIfMediumEnemyExistsInSlotTwo);

        }
        if (largeEnemies.Count() == 1)
        {
            forbiddenSmallEnemyIndices.AddRange(ForbiddenSmallEnemyIndicesIfLargeEnemyExists);
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

    public bool IsRoomForAnotherEnemy()
    {
        return !GetAvailableSpotsForNewSmallUnits().IsEmpty();
    }

    public CreateEnemyResult CreateNewEnemyAndRegisterWithGamestate(AbstractBattleUnit battleUnit)
    {
        var firstEmptyBattleUnitHolder = GetAvailableSpotsForNewSmallUnits()[0];
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
