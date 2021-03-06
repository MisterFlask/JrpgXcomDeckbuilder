﻿using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies
{
    public class EnemyFaction : MonoBehaviour
    {
        public string FactionName { get; set; }

        public static EnemyFaction COLUMBAL = new EnemyFaction() { FactionName = "Columbal Transit Authority" };
        public static EnemyFaction EFFICIENCY = new EnemyFaction() { FactionName = "The Church of the Efficiency" };
        public static EnemyFaction CIRCLE = new EnemyFaction() { FactionName = "The Pyramid" };

        public static EnemyFaction NONE = new EnemyFaction() { FactionName = "No Faction" };
    }
}