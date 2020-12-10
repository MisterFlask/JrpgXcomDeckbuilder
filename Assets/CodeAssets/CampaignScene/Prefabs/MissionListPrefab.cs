using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionListPrefab : MonoBehaviour
{
    public Transform MissionsParent;
    public SelectableMissionPrefab PrefabTemplate;

    public List<SelectableMissionPrefab> ChildPrefabs { get; set; } = new List<SelectableMissionPrefab>();

    public static MissionListPrefab Instance => GameObject.FindObjectOfType<MissionListPrefab>();

    // Use this for initialization
    void Start()
    {
    }

    public void Initialize()
    {
        MissionsParent.gameObject.PurgeChildren();
        foreach(var mission in CampaignMapState.MissionsActive)
        {
            AddMission(mission);
        }
    }

    public void AddMission(Mission mission)
    {
        var missionPrefab = PrefabTemplate.Spawn(MissionsParent);
        missionPrefab.Mission = mission;
        ChildPrefabs.Add(missionPrefab);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
