using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionGenerator
{
    int missionsGenerated = 0;
    public Mission GenerateNewMission()
    {
        missionsGenerated += 1;
        return new KillEnemiesMission() {
            Squad = Squad.PredefinedSquads.PickRandom(),
            Difficulty = 1,
            MoneyReward = 50,
            Name = $"Mission {missionsGenerated}"
        };
    }
}


public class KillEnemiesMission: Mission
{
    public int MoneyReward { get; set; } = 50;
    public Squad Squad { get; set; }

    public override void OnSuccess()
    {
        GameState.Instance.money += MoneyReward;
    }

    public override List<AbstractBattleUnit> StartingEnemies()
    {
        Squad.SetDifficulty(Difficulty);
        return Squad.Members;
    }
}

public class Squad
{
    public List<AbstractBattleUnit> Members { get; set; }
    
    public void SetDifficulty(int difficulty)
    {
        foreach(var guy in Members)
        {
            guy.SetDifficulty(difficulty);
        }
    }

    public static List<Squad> PredefinedSquads = new List<Squad>
    {
        new Squad
        {
            Members = new List<AbstractBattleUnit>
            {
                new Greywing(),
                new Greywing(),
                new Greywing(),
                new Greywing()
            }
        }
    };
}
