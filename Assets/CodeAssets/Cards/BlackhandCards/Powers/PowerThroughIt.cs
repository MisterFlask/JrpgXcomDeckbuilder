using UnityEngine;
using System.Collections;
using System;

public class PowerThroughIt : AbstractCard
{
    public override string Description()
    {
        return "Whenever targeted ally loses HP, they recover up to 5 HP and 5 Stress.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().ApplyStatusEffect(target, new PowerThroughItStatusEffect(), 5);
    }
}


public class PowerThroughItStatusEffect : AbstractStatusEffect
{
    public PowerThroughItStatusEffect()
    {
        Name = "Power Through It";
    }

    public override void ModifyDamage(DamageBlob damageBlob)
    {
        int damageMitigated = Math.Min(damageBlob.Damage, this.Stacks);

        damageBlob.Damage -= damageMitigated;

        if (!damageBlob.IsDamagePreview)
        {
            action().ApplyStatusEffect(OwnerUnit, new StressStatusEffect(), damageMitigated);
        }
    }


    public override string Description => $"Whenever you lose HP, mitigate it by up to {Stacks} health and take that much stress.";
}