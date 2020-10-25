﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleResultsScreenPrefab : MonoBehaviour
{
    public Button BackToCampaignButton;
    public TMPro.TextMeshProUGUI MissionCompletionSummaryText;
    // Use this for initialization
    void Start()
    {
        BackToCampaignButton.onClick.AddListener(delegate {
            GameScenes.SwitchToCampaignScene();
        });

    }

    // Update is called once per frame
    void Update()
    {

    }
}