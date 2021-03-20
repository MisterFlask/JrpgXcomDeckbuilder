using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class DonkeyKick : AbstractCard
{
    public DonkeyKick()
    {
        SoldierClassCardPools.Add(typeof(VanguardSoldierClass));
        Name = "Donkey Kick";
        BaseDamage = 14;
        TargetType = TargetType.ENEMY;
        CardType = CardType.AttackCard;
    }

    public override int BaseEnergyCost()
    {
        if (Owner == null)
        {
            return 2;
        }

        if (Owner.HasStatusEffect<AdvancedStatusEffect>())
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        Require.NotNull(target);
        action().AttackUnitForDamage(target, Owner, BaseDamage, this);
    }

    public override string DescriptionInner()
    {
        return $"Deals {displayedDamage()} damage to an enemy unit.  Costs 1 less if Advanced.";
    }
}
