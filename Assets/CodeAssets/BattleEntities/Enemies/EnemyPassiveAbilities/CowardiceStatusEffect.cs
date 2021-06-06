using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    /// <summary>
    /// If this unit is struck three times in a single round,
    /// it flees the combat.  Right now we're just saying it dies.
    /// </summary>
    public class CowardiceStatusEffect : AbstractStatusEffect
    {
        public  CowardiceStatusEffect()
        {
            Name = "Cowardice";
        }

        public override void OnRemoval()
        {
            ActionManager.Instance.KillUnit(OwnerUnit);
        }

        public override void OnTurnStart()
        {
            Stacks = 3;
        }

        public override void OnStruck(AbstractBattleUnit unitStriking, int totalDamageTaken)
        {
            Stacks--;
        }

        public override string Description => $"If this unit is struck three times in a single round, it flees.";
    }
}