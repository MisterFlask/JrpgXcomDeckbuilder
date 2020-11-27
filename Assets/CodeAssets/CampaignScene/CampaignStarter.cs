using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CampaignStarter : MonoBehaviour
{

    public void InitializeCampaign()
    {
        InitializeSelectableMissions();
        InitializeRoster();
        CampaignMapState.GameInitialized = true;
    }

    public static void InitializeRoster()
    {
        CampaignMapState.Roster = new List<AbstractBattleUnit>
        {
            Soldier.Generate(),
            Soldier.Generate(),
            Soldier.Generate(),
            Soldier.Generate(),
            GetHigherLevelSoldier(),
            GetDeadSoldier()
        };
    }

    private static AbstractBattleUnit GetDeadSoldier()
    {
        var soldier = Soldier.Generate();
        soldier.CombatsParticipatedIn = 10;
        soldier.NumberCardRewardsEligibleFor = 1;
        return soldier;
    }

    private static AbstractBattleUnit GetHigherLevelSoldier()
    {

        var soldier = Soldier.Generate();
        soldier.CombatsParticipatedIn = 10;
        soldier.NumberCardRewardsEligibleFor = 1;
        soldier.CurrentHp = 0;
        return soldier;
    }

    public static void InitializeSelectableMissions()
    {
        CampaignMapState.MissionsSelectable = new List<Mission>
        {
            CampaignMapState.MissionGenerator.GenerateNewMission(),
            CampaignMapState.MissionGenerator.GenerateNewMission(),
            CampaignMapState.MissionGenerator.GenerateNewMission()
        };
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
