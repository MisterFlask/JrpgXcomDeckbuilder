using UnityEngine;
using System.Collections;

public class FlankingShot : AbstractCard
{
    public FlankingShot()
    {
        Name = "Flanking Shot";
        CardType = CardType.AttackCard;
        TargetType = TargetType.ENEMY;
        BaseDamage = 2;
    }

    public override int BaseEnergyCost()
    {
        return 0;
    }

    public override string Description()
    {
        return $"Deal {displayedDamage()} damage.  Apply 1 Vulnerable.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().AttackUnitForDamage(target, Owner, BaseDamage);
        action().ApplyStatusEffect(target, new VulnerableStatusEffect(), 1);
        action().ExpendCard(this);
    }
}
