using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class AbominationStatusEffect : AbstractStatusEffect
    {
        public AbominationStatusEffect()
        {

            Name = "Abomination";
        }

        public override string Description => "Whenever this deals unblocked damage, apply [stacks] stress to the character.";


        public override void OnStriking(AbstractBattleUnit unitStruck, AbstractCard cardUsedIfAny, int damageAfterBlockingAndModifiers)
        {
            if (damageAfterBlockingAndModifiers > 0)
            {
                ActionManager.Instance.ApplyStress(unitStruck, stressApplied: Stacks);
            }
        }
    }
}