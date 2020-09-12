using UnityEngine;
using System.Collections;

public class Bayonet : AbstractCard
{
    public Bayonet()
    {
        Name = "Bayonet";
    }

    public override int BaseEnergyCost()
    {
        if (Owner.HasStatusEffect<AdvancedStatusEffect>())
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().AttackUnitForDamage(target, Owner, BaseDamage);
    }

    public override string Description()
    {
        return $"Deals {displayedDamage()} damage to an enemy unit.  Costs 1 less if Advanced.";
    }
}
