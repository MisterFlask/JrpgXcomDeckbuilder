using UnityEngine;
using System.Collections;

public class DonkeyKick : AbstractCard
{
    public DonkeyKick()
    {
        Name = "Donkey Kick";
        BaseDamage = 14;
        TargetType = TargetType.ENEMY;
        CardType = CardType.AttackCard;
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
        Require.NotNull(target);
        action().AttackUnitForDamage(target, Owner, BaseDamage);
    }

    public override string Description()
    {
        return $"Deals {displayedDamage()} damage to an enemy unit.  Costs 1 less if Advanced.";
    }
}
