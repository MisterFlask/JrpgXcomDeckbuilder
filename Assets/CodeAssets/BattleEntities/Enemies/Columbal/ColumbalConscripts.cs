using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.Columbal
{
    public class ColumbalConscripts : AbstractEnemyUnit
    {
        public ColumbalConscripts()
        {
            EnemyFaction = EnemyFaction.COLUMBAL;
        }

        public override List<AbstractIntent> GetNextIntents()
        {

        }
    }
}