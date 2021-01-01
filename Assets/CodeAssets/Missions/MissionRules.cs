using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static MissionGenerator;
using System;

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
            ActOne, 
            
            
            new List<Squad>{ 
            new Squad
            {
                Members = new List<AbstractBattleUnit>
                {
                    new BrainCrab(),
                    new BrainCrab()
                }
            },
            new Squad
            {
                Members = new List<AbstractBattleUnit>
                {
                    new UnitThatDealsDamageWhenAttackedMultipleTimesInATurn()
                }
            }
        } 
        },
        { 
            ActTwo, new List<Squad>{
            new Squad
            {
                Members = new List<AbstractBattleUnit>
                {
                    new UnitThatDealsDamageWhenAttackedMultipleTimesInATurn(),
                    new UnitThatDealsDamageWhenAttackedMultipleTimesInATurn(),
                    new UnitThatAppliesDazedWhenStruck()
                },
            },
            new Squad{
                Members = new List<AbstractBattleUnit>
                {

                    new UnitThatAppliesDazedWhenStruck(),
                    new BrainCrab(),
                    new UnitThatAppliesDazedWhenStruck(),
                    new BrainCrab()
                }
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
        return squads;
    }

    public static Squad GetRandomSquadForDay(int dayMissionSpawned)
    {
        return GetPossibleEnemySquadsForDay(dayMissionSpawned).First();
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