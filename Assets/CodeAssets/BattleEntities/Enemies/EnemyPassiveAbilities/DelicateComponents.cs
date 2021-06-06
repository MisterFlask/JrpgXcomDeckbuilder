using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class DelicateComponents : AbstractStatusEffect
    {
        public override string Description => "When this unit dies, add a Common Augment into your inventory.  Removed if this enemy takes >[Stacks] damage on a single turn.";

        public DelicateComponents()
        {
            Name = "Delicate Components";
        }

        public override void OnTurnStart()
        {
            SecondaryStacks = 0;
        }

        public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
        {
            SecondaryStacks += totalDamageTaken;
            if (SecondaryStacks >= Stacks)
            {
                Stacks = 0;
            }
        }
    }
}