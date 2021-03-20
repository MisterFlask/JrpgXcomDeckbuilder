using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public static class MissionGenerator
{
    public static List<Mission> GenerateAllMissionsForDay()
    {
        var dayNumber = GameState.Instance.Day;

        return new List<Mission>
        {

            new KillEnemiesMission()
            {
                DaysUntilExpiration = 3,
                Difficulty = 1,
                MaxNumberOfFriendlyCharacters = 3,
                MoneyReward = 50,
                Name = Mission.GenerateMissionName(),
                Rewards = new List<AbstractMissionReward>{new GoldMissionReward(50)},
                EnemySquad = MissionRules.GetRandomSquadForDay(dayNumber)
    },
            new KillEnemiesMission()
            {
                DaysUntilExpiration = 3,
                Difficulty = 1,
                MaxNumberOfFriendlyCharacters = 3,
                MoneyReward = 60,
                Name = Mission.GenerateMissionName(),
                Rewards = new List<AbstractMissionReward>{new GoldMissionReward(60)},
                EnemySquad = MissionRules.GetRandomSquadForDay(dayNumber)
            }
        };
    }
}


public class KillEnemiesMission: Mission
{
    public int MoneyReward { get; set; } = 50;
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

    public override void OnSuccess()
    {
        GameState.Instance.Credits += MoneyReward;
    }
}

public class Squad
{
    public List<AbstractBattleUnit> Members { get; set; }
    public int BaseDifficulty { get; set; }

    public void SetDifficulty(int difficulty)
    {
        foreach(var guy in Members)
        {
            guy.SetDifficulty(difficulty - BaseDifficulty);
        }
    }
}


public abstract class MissionModifier
{
    public abstract void OnMissionCombatBegins();
    public abstract string Description();
}



