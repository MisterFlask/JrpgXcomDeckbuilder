using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class Toughness : AbstractStatusEffect
    {
        public override string Description => "Reduces damage by 1 per stack.";

        public override int DamageReceivedAddition()
        {
            return -1 * Stacks;
        }
    }
}