using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class AppalledStatusEffect : AbstractStatusEffect
    {
        // lose [Stacks] strength each time attacked, to a minimum of -3.  Penalty decreases each turn.
        public override string Description => "Lose [stacks] strength each time attacked.  " +
            "Strength reset to 0 at start of turn.";


            public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
        {
            ActionManager.Instance.ApplyStatusEffect(OwnerUnit, new StrengthStatusEffect(), -1);
        }

        public override void OnTurnStart()
        {
            OwnerUnit.RemoveStatusEffect<StrengthStatusEffect>();
        }
    }
}