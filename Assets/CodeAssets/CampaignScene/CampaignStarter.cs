using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.CodeAssets.BattleEntities.Augmentations;
using Assets.CodeAssets.GameLogic;
using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;

public class CampaignStarter : MonoBehaviour
{

    public void InitializeCampaign()
    {
        MagicWord.RegisterMagicWordsReflectively();
        CardRegistrar.InitCardsReflectively();
        InitializeSelectableMissions();
        InitializeRoster();
        CampaignMapState.GameInitialized = true;
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

        foreach (var soldier in CampaignMapState.Roster) {
            soldier.ApplySoldierPerk(new DealGreaterDamageToEnemiesWithStatusEffectPerk(new BurningStatusEffect(), 1, "Firefighter"));
        }
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
        CampaignMapState.MissionsActive = MissionGenerator.GenerateAllMissionsForRegion();
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
