using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public static class MissionGenerator
{
    public static List<AbstractMission> GenerateAllMissionsForRegion()
    {
        var dayNumber = GameState.Instance.Day;

        return new List<AbstractMission>
        {
            new KillEnemiesMission()
            {
                DaysUntilExpiration = 1000,
                Difficulty = 1,
                MaxNumberOfFriendlyCharacters = 3,
                Name = AbstractMission.GenerateMissionName(),
                Rewards = new List<AbstractMissionReward>{new GoldMissionReward(60)},
                EnemySquad = MissionRules.GetRandomSquadForDay(dayNumber)
            },
            new KillEnemiesMission()
            {
                DaysUntilExpiration = 1000,
                Difficulty = 1,
                MaxNumberOfFriendlyCharacters = 3,
                Name = AbstractMission.GenerateMissionName(),
                Rewards = new List<AbstractMissionReward>{new GoldMissionReward(60)},
                EnemySquad = MissionRules.GetRandomSquadForDay(dayNumber)
            },
            new KillEnemiesMission()
            {
                DaysUntilExpiration = 1000,
                Difficulty = 4,
                MaxNumberOfFriendlyCharacters = 3,
                Name = AbstractMission.GenerateMissionName(),
                Rewards = new List<AbstractMissionReward>{new GateBypassMissionReward()},
                EnemySquad = MissionRules.GetEliteSquad()
            }
        };
    }
}


public class KillEnemiesMission: AbstractMission
{
    public string Description { get; set; }

    public string GetDescription()
    {
        if (Description == null)
        {
            var names = EnemySquad.Members.Select(item => item.CharacterFullName);
            return string.Join(",", names);
        }
        return Description;
    }
}

public class Squad
{
    public List<AbstractBattleUnit> Members { get; set; }
    public int BaseDifficulty { get; set; }

    public string Description { get; set; }

    public void SetDifficulty(int difficulty)
    {
        foreach(var guy in Members)
        {
            guy.SetDifficulty(difficulty - BaseDifficulty);
        }
    }

    internal Squad CopySquad()
    {
        return new Squad
        {
            BaseDifficulty = BaseDifficulty,
            Description = Description,
            Members = Members.Select(item => item.CloneUnit()).ToList()
        };
    }
}


public abstract class MissionModifier
{
    public abstract void OnMissionCombatBegins();
    public abstract string Description();
}



