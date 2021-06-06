using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class BeautiousStatusEffect : AbstractStatusEffect
    {
        public BeautiousStatusEffect()
        {
            Name = "Beautious";
        }

        public override string Description => "It costs [Stacks] extra energy to target this unit.";

        public override int GetTargetedCostModifier(AbstractCard card)
        {
            return Stacks;
        }
    }
}