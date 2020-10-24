﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CampaignStarter : MonoBehaviour
{

    public void InitializeCampaign()
    {
        InitializeSelectableMissions();
        InitializeRoster();
        CampaignMapState.Initialized = true;
    }

    public static void InitializeRoster()
    {
        CampaignMapState.Roster = new List<AbstractBattleUnit>
        {
            Rookie.Generate(),
            Rookie.Generate(),
            Rookie.Generate(),
            Rookie.Generate()
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
        if (!CampaignMapState.Initialized)
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
