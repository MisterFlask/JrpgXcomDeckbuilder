using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class AnnoyanceStatusEffect : AbstractStatusEffect
    {
        public override string Description => "This character gains 1 strength each time it's attacked.";


            public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
        {
            ActionManager.Instance.ApplyStatusEffect(OwnerUnit, new StrengthStatusEffect(), 1);

        }
    }
}