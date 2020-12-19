using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

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
        var missionsRepresentedByPrefabs = ChildPrefabs.Select(item => item.Mission).ToList();

        // first, ensure we have all prefabs needed and all those we don't have
        foreach(var mission in CampaignMapState.MissionsActive)
        {
            if (!missionsRepresentedByPrefabs.Contains(mission)){
                AddMission(mission);
            }
        }

        //now remove (deparent, really) all prefabs unnecessary
        foreach (var mission in missionsRepresentedByPrefabs)
        {
            if (!CampaignMapState.MissionsActive.Contains(mission))
            {
                var obj = GetMissionPrefab(mission);
                obj.transform.SetParent(null);
            }
        }

    }

    private GameObject GetMissionPrefab(Mission mission)
    {
        return ChildPrefabs.FirstOrDefault(item => item.Mission == mission).gameObject;
    }
}
