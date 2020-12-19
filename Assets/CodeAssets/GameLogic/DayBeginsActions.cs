using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayBeginsActions : MonoBehaviour
{

    public static void RotateMissions()
    {
        foreach (var mission in CampaignMapState.MissionsActive)
        {
            mission.DaysUntilExpiration--;
            if (mission.DaysUntilExpiration == 0)
            {
                mission.OnFailed();
            }
        }

        //now remove them from the active missions list.
        CampaignMapState.MissionsActive.RemoveAll(item => item.DaysUntilExpiration <= 0);

        var newMissions = MissionGenerator.GenerateAllMissionsForDay();
        CampaignMapState.MissionsActive.AddRange(newMissions);
    }

    public static void ApplyTriggers()
    {
        foreach(var character in GameState.Instance.PersistentCharacterRoster)
        {
            foreach(var perk in character.Perks)
            {
                perk.PerformAtBeginningOfNewDay(character);
            }
        }
    }

    public void StartANewDay()
    {
        GameState.Instance.Day++;
        RotateMissions();
        ApplyTriggers();
    }


}

