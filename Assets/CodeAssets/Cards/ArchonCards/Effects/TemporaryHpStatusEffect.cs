using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Effects
{
    public class TemporaryHpStatusEffect : AbstractStatusEffect
    {
        public override string Description => "Temporary HP";

        public override void ModifyPostBlockDamageTaken(DamageBlob damageBlob)
        {
            if (damageBlob.Damage >= Stacks)
            {
                damageBlob.Damage -= this.Stacks;
                this.Stacks = 0;
            }
            else
            {
                this.Stacks -= damageBlob.Damage;
                damageBlob.Damage = 0;
            }
        }
    }
}