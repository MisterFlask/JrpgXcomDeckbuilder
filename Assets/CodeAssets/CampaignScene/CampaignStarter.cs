using UnityEngine;
using System.Collections;

public class CampaignStarter : MonoBehaviour
{

    public void InitializeCampaign()
    {
        CampaignMapState.InitializeSelectableMissions();
        CampaignMapState.InitializeRoster();
    }

    // Use this for initialization
    void Start()
    {
        InitializeCampaign();
        MissionListPrefab.Instance.Initialize();
        RosterPrefab.Instance.Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
