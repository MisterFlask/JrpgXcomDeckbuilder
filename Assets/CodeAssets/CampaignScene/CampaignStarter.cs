using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            Soldier.Generate()
        };
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
