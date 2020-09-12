using UnityEngine;
using System.Collections;

public class Grenade : AbstractCard
{
    public Grenade()
    {
        this.BaseDamage = 5;
        TargetType = TargetType.ENEMY;
        Name = "Grenade";
    }

    public override string Description()
    {
        return $"Deals {displayedDamage()} damage to the target, then {displayedDamage()} damage to all enemies.  Expend.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage);

        foreach(var otherTarget in enemies())
        {
            action().AttackUnitForDamage(otherTarget, this.Owner, BaseDamage);
        }
        action().ExpendCard(this);
    }
}
