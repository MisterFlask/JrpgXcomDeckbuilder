﻿using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies
{
    public class EnemySquadRole
    {
        public string Name { get; set; }

        public static EnemySquadRole LARGE = new EnemySquadRole { Name = "Large" };
        public static EnemySquadRole REGULAR = new EnemySquadRole { Name = "Regular" };
        public static EnemySquadRole SUPPORT = new EnemySquadRole { Name = "Support" };
    }
}