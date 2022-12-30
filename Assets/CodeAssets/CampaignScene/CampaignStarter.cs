using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.CodeAssets.BattleEntities.Augmentations;
using Assets.CodeAssets.GameLogic;
using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Utils;

public class CampaignStarter : MonoBehaviour
{
    public ShowDeckScreen ShowDeckScreen;



    public void InitializeCampaign()
    {
        MagicWord.RegisterMagicWordsReflectively();
        CardRegistrar.InitCardsReflectively();
        InitializeSelectableMissions();
        InitializeRoster();
        CampaignMapState.GameInitialized = true;
        ShowDeckScreen.Start(); // done to ensure that Instance variable gets initialized
    }

    public static void InitializeRoster()
    {
        CampaignMapState.Roster = new List<AbstractBattleUnit>
        {
            Soldier.GenerateFreshRookie(),
            Soldier.GenerateFreshRookie(),
            Soldier.GenerateSoldierOfClass(new BlackhandSoldierClass()),
            Soldier.GenerateSoldierOfClass(new DiabolistSoldierClass()),
            Soldier.GenerateSoldierOfClass(new CogSoldierClass()),
            GetHigherLevelSoldier(),
            GetDeadSoldier()
        };
    }

    private static AbstractBattleUnit GetDeadSoldier()
    {
        var soldier = Soldier.GenerateFreshRookie();
        soldier.CombatsParticipatedIn = 10;
        soldier.NumberCardRewardsEligibleFor = 1;
        soldier.CurrentHp = 0;
        return soldier;
    }

    private static AbstractBattleUnit GetHigherLevelSoldier()
    {
        var soldier = Soldier.GenerateFreshRookie();
        soldier.CombatsParticipatedIn = 10;
        soldier.NumberCardRewardsEligibleFor = 1;
        return soldier;
    }

    public static void InitializeSelectableMissions()
    {
        // CampaignMapState.MissionsActive = MissionGenerator.GenerateAllMissionsForRegion();
    }

    void Awake()
    {
        EagerMonobehaviour.InitializeAllEagerMonobehaviours();
    }

    // Use this for initialization
    void Start()
    {
        if (!CampaignMapState.GameInitialized)
        {
            InitializeCampaign();
        }
        CampaignMapState.InitializeCampaignScreen();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
