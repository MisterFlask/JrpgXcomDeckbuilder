using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class StartingMissionGenerator
{
    public static List<Mission> GetStartingMissions()
    {
        return new List<Mission>
        {
            new KillEnemiesMission() {
                Squad = Squad.PredefinedSquads.PickRandom(),
                Difficulty = 1,
                Name = Mission.GenerateMissionName(),
                Rewards = new List<AbstractMissionReward> { new GoldMissionReward(50) }
            },
            new KillEnemiesMission() {
                Squad = Squad.PredefinedSquads.PickRandom(),
                Difficulty = 1,
                Name = Mission.GenerateMissionName(),
                Rewards = new List<AbstractMissionReward> { new GoldMissionReward(50) }
            },
            new KillEnemiesMission() {
                Squad = Squad.PredefinedSquads.PickRandom(),
                Difficulty = 1,
                Name = Mission.GenerateMissionName(),
                Rewards = new List<AbstractMissionReward> { new GoldMissionReward(50) }
            }
        };
    }
}

public class ProbabilisticMissionGenerator: MissionGenerator
{
    int missionsGenerated = 0;
    public Mission GenerateMission()
    {
        missionsGenerated += 1;
        return new KillEnemiesMission() {
            Squad = Squad.PredefinedSquads.PickRandom(),
            Difficulty = 1,
            Name = Mission.GenerateMissionName(),
            Rewards = new List<AbstractMissionReward> { new GoldMissionReward(50) }
        };
    }

    float Probability = .3f;

    public bool ShouldGenerateMissionThisDay(int currentDay)
    {
        return Random.Range(0.0f, 1.0f) < Probability;
    }
}

public interface MissionGenerator{
    bool ShouldGenerateMissionThisDay(int currentDay);
    Mission GenerateMission();
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
    public int BaseDifficulty { get; set; }

    public void SetDifficulty(int difficulty)
    {
        foreach(var guy in Members)
        {
            guy.SetDifficulty(difficulty - BaseDifficulty);
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
