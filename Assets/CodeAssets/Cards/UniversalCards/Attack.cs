using UnityEngine;
using System.Collections;

public class Attack : AbstractCard
{
    public Attack()
    {
        BaseDamage = 6;
    }

    public override string Description()
    {
        return $"Deal {DisplayedDamage()} damage";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage);
    }
}
