using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static MissionGenerator;
using System;
using Assets.CodeAssets.BattleEntities.Enemies.Columbal;
using Assets.CodeAssets.BattleEntities.Enemies.Efficiency;

public static class MissionRules
{
    public static GameAct ActOne = new GameAct 
    {
        startingDay = 0,
        endingDay = 7
    };

    public static GameAct ActTwo = new GameAct
    {
        startingDay = 8,
        endingDay = 15
    };

    public static List<GameAct> DayRanges = new List<GameAct>
    {
        ActOne, ActTwo
    };


    /// <summary>
    /// Note: Requires at least two squads per act.
    /// </summary>
    public static Dictionary<GameAct, List<Squad>> ActToSquadsMet => new Dictionary<GameAct, List<Squad>>
    {
        { 
            ActOne, new List<Squad>{ 
            new Squad
            {
                Members = new List<AbstractBattleUnit>
                {
                    new ColumbalConscripts(),
                    new ColumbalConscripts()
                },
                Description = "An inconvenience of geese."
            },
            new Squad
            {
                Members = new List<AbstractBattleUnit>
                {
                    new EfficiencyProselytizer(),
                    new EfficiencySpotter()
                },
                Description = "Efficiency scouting party"
            }
        } 
        },
        { 
            ActTwo, new List<Squad>{
            new Squad
            {
                Members = new List<AbstractBattleUnit>
                {
                    new ColumbalConscripts(),
                    new ColumbalConscripts()
                },
                Description = "An inconvenience of geese."
            },
            new Squad
            {
                Members = new List<AbstractBattleUnit>
                {
                    new EfficiencyProselytizer(),
                    new EfficiencySpotter()
                },
                Description = "Efficiency scouting party"
            }
        } },
    };


    public static List<Squad> GetPossibleEnemySquadsForDay(int day)
    {

        var appropriateDayRanges = DayRanges.Where(item => item.DayWithinRange(day));
        var squads = appropriateDayRanges.Select(item => ActToSquadsMet[item]).SelectMany(item => item).ToList();
        if (squads.Count == 0)
        {
            throw new Exception("Couldn't find any squads for day " + day);
        }

        // modify squads based on difficulty setting [1-10]
        return squads;
    }

    public static Squad GetRandomSquadForDay(int dayMissionSpawned)
    {
        return GetPossibleEnemySquadsForDay(dayMissionSpawned).PickRandom().CopySquad();
    }

    public static Squad GetEliteSquad()
    {
        return GetPossibleBossSquads();
    }

    private static Squad GetPossibleBossSquads()
    {
        return new List<Squad>{
            new Squad
            {
                Members = new List<AbstractBattleUnit>
                {
                    new ColumbalConscripts(),
                    new ColumbalConscripts(),
                    new ColumbalConscripts(),
                    new ColumbalConscripts()
                },
                Description = "A devastation of geese."
            },
            new Squad
            {
                Members = new List<AbstractBattleUnit>
                {
                    new EfficiencyProselytizer(),
                    new EfficiencySpotter(),
                    new EfficiencySpotter(),
                    new EfficiencySubduer()
                },
                Description = "Efficiency hunting party"
            }
        }.PickRandom();
    }
}

public class GameAct
{
    public int startingDay { get; set; }
    public int endingDay { get; set; }

    public bool DayWithinRange(int day)
    {
        return day >= startingDay && day <= endingDay;
    }
}