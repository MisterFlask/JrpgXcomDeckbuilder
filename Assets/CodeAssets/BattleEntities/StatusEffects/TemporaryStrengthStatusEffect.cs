using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.StatusEffects
{
    public class TemporaryStrengthStatusEffect : AbstractStatusEffect
    {
        public override string Description => $"Deal {DisplayedStacks()} more damage with attacks.";

        public override int DamageDealtAddition()
        {
            return Stacks;
        }

        public override void OnTurnEnd()
        {
            this.Stacks = 0;
        }
    }
}