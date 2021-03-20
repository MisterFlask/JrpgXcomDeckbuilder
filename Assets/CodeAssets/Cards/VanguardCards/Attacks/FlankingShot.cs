using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class FlankingShot : AbstractCard
{
    public FlankingShot()
    {
        SoldierClassCardPools.Add(typeof(VanguardSoldierClass));
        Name = "Flanking Shot";
        CardType = CardType.AttackCard;
        TargetType = TargetType.ENEMY;
        BaseDamage = 2;
    }

    public override int BaseEnergyCost()
    {
        return 0;
    }

    public override string DescriptionInner()
    {
        return $"Deal {displayedDamage()} damage.  Apply 1 Vulnerable.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().AttackUnitForDamage(target, Owner, BaseDamage, this);
        action().ApplyStatusEffect(target, new VulnerableStatusEffect(), 1);
        action().ExhaustCard(this);
    }
}
