using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static MissionGenerator;
using System;
using Assets.CodeAssets.BattleEntities.Enemies.Columbal;
using Assets.CodeAssets.BattleEntities.Enemies.Efficiency;
using Assets.CodeAssets.BattleEntities.Enemies.UnitSquad;

public static class MissionRules
{

    public static Squad GetRandomSquadForCurrentActAndDay(SquadType squadType)
    {
        var act = GameState.Instance.Act;
        var doomCounter = GameState.Instance.DoomCounter.CurrentDoomCounter;

        return GameAct.GetSquadForAct(act, squadType);
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
