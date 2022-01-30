using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.CodeAssets.Utils;
using Map;

public static class MissionGenerator
{

    internal static AbstractMission GenerateMissionForNode(MapNode entered)
    {
        if (entered.Node.nodeType == NodeType.MinorEnemy)
        {
            return new KillEnemiesMission()
            {
                DaysUntilExpiration = 1000,
                Difficulty = 1,
                MaxNumberOfFriendlyCharacters = 3,
                Name = AbstractMission.GenerateMissionName(),
                Rewards = new List<AbstractMissionReward> { new GoldMissionReward(60) },
                EnemySquad = MissionRules.GetRandomSquadForCurrentActAndDay(SquadType.NORMAL),
                ProtoSprite = AbstractMission.RetrieveIconFromMissionIconFolder("cash"),
                MissionModifiers = GetRandomMissionModifiers()
            };
        }
        if (entered.Node.nodeType == NodeType.EliteEnemy)
        {
            return new KillEnemiesMission()
            {
                DaysUntilExpiration = 1000,
                Difficulty = 1,
                MaxNumberOfFriendlyCharacters = 3,
                Name = AbstractMission.GenerateMissionName(),
                Rewards = new List<AbstractMissionReward> { new GoldMissionReward(60) },
                EnemySquad = MissionRules.GetRandomSquadForCurrentActAndDay(SquadType.ELITE),
                ProtoSprite = AbstractMission.RetrieveIconFromMissionIconFolder("cash"),
                MissionModifiers = GetRandomMissionModifiers()
            };
        }
        if (entered.Node.nodeType == NodeType.Boss)
        {
            return new KillEnemiesMission()
            {
                DaysUntilExpiration = 1000,
                Difficulty = 1,
                MaxNumberOfFriendlyCharacters = 3,
                Name = AbstractMission.GenerateMissionName(),
                Rewards = new List<AbstractMissionReward> { new GoldMissionReward(60) },
                EnemySquad = MissionRules.GetRandomSquadForCurrentActAndDay(SquadType.NORMAL),
                ProtoSprite = AbstractMission.RetrieveIconFromMissionIconFolder("cash"),
                MissionModifiers = GetRandomMissionModifiers()
            };
        }

        throw new System.Exception("");
    }

    private static List<MissionModifier> GetRandomMissionModifiers()
    {
        return new List<MissionModifier>()
        {
            MissionModifier.GetRandomMissionModifier()
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
    public int BaseDifficulty { get; set; } = 0;
    public string Description { get; set; } = "???";

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
    public static MissionModifier GetRandomMissionModifier()
    {
        return new List<MissionModifier>()
        {
            new DarknessMissionModifier(),
            new HighWindsMissionModifier(),
            new NoxiousGasesMissionModifier()
        }.PickRandom();
    }

    public virtual int IncrementalMoney()
    {
        return 0;
    }


    public abstract void OnMissionCombatBegins();
    public abstract string Description();
}



