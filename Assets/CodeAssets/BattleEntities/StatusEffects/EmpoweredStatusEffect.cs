using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.StatusEffects
{
    public class EmpoweredStatusEffect : AbstractStatusEffect
    {
        public override string Description => $"For the next {DisplayedStacks()} turns, this character deals 2 additional damage and applies 2 additional defense.";

        // For the next [stacks] turns, this character deals 2 additional damage and applies 2 additional defense.

        public EmpoweredStatusEffect()
        {
            Name = "Empowered";
        }


        public override void OnTurnStart()
        {
            Stacks--;
        }


        public override int DamageDealtAddition()
        {
            return 2;
        }

        public override int DefenseDealtAddition()
        {
            return 2;
        }
    }
}