using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DayBeginsActions
{

    public static List<MissionGenerator> MissionGenerators = new List<MissionGenerator>{
        new ProbabilisticMissionGenerator()
    };

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

        foreach (var generator in MissionGenerators)
        {
            if (generator.ShouldGenerateMissionThisDay(GameState.Instance.Day))
            {
                var mission = generator.GenerateMission();
                CampaignMapState.MissionsActive.Add(mission);
            }
        }
        
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
}

