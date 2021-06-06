using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class FlightStatusEffect : AbstractStatusEffect
    {
        public FlightStatusEffect()
        {
            Name = "Flying";
        }

        public override string Description => "Deals [stacks] extra damage.  Remove Flight after being hit three times in a turn.";

        public override void OnStruck(AbstractBattleUnit unitStriking, int totalDamageTaken)
        {
            Stacks--;
        }

        public override void OnTurnStart()
        {
            Stacks = 3;
            OwnerUnit.RemoveStatusEffect<FlightEffectOnFirstHitTakenThisTurn>();
            OwnerUnit.ApplyStatusEffect(new FlightEffectOnFirstHitTakenThisTurn(), 1);
        }
    }

    public class FlightEffectOnFirstHitTakenThisTurn : AbstractStatusEffect
    {
        public FlightEffectOnFirstHitTakenThisTurn()
        {
            Name = "Evasive (Flying)";
        }

        public override string Description => "The next attack this turn deals 1 damage.";

        public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
        {
            Stacks--;
        }
    }
}