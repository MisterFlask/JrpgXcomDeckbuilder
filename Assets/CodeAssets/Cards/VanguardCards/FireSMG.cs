using UnityEngine;
using System.Collections;

public class FireSMG : AbstractCard
{
    public FireSMG()
    {
        BaseDamage = 5;
    }

    public override string Description()
    {
        return $"Deal {DisplayedDamage()}.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().AttackUnitForDamage(target, Owner, BaseDamage);
    }
}
