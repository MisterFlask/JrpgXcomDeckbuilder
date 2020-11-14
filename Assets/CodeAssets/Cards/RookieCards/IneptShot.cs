using UnityEngine;
using System.Collections;

public class IneptShot : AbstractCard
{
    public IneptShot()
    {
        BaseDamage = 3;
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
