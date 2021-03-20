using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Effects
{
    public class TerrorStatusEffect : AbstractStatusEffect
    {
        public TerrorStatusEffect()
        {
            this.Name = "Terror";
        }
        public override string Description => "At the end of your turn, if a unit's Terror is greater than its HP, it dies.  Otherwise, it loses half of its Terror.";

        public override void OnTurnEnd()
        {
            if (this.Stacks > this.OwnerUnit.CurrentHp)
            {
                action().KillUnit(this.OwnerUnit);
            }
            else
            {
                action().ApplyStatusEffect(this.OwnerUnit, new TerrorStatusEffect(), (int) (-0.5 * this.Stacks));
            }
        }

    }
}