using UnityEngine;
using System.Collections;

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

    public override string Description()
    {
        return $"Deals {displayedDamage()} damage to the target.  Apply 1 Weak.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage);
        action().ApplyStatusEffect(target, new WeakenedStatusEffect());
    }
}
