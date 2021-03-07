using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class CoveringFire : AbstractCard
{

    public CoveringFire()
    {
        SoldierClassCardPools.Add(typeof(RookieClass));
        this.BaseDamage = 2;
        TargetType = TargetType.ENEMY;
        CardType = CardType.AttackCard;
        Name = "Covering Fire";
    }

    public override int BaseEnergyCost()
    {
        return 0;
    }

    public override string DescriptionInner()
    {
        return $"Deals {displayedDamage()} damage to the target.  Apply 1 Weak.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage);
        action().ApplyStatusEffect(target, new WeakenedStatusEffect());
    }
}
