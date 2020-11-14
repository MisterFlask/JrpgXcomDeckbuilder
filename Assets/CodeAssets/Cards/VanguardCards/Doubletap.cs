using UnityEngine;
using System.Collections;

public class Doubletap : AbstractCard
{
    public Doubletap()
    {
        BaseTechValue = 1;
    }

    public override string Description()
    {
        return $"Deal {DisplayedDamage()}, twice.  Slay: Gain {BaseTechValue} Power.";
    }
    protected override void OnPlay(AbstractBattleUnit target)
    {
        ActionManager.Instance.AttackUnitForDamage(target, Owner, 2);
        ActionManager.Instance.DoAThing(() =>
        {
            if (target.IsDead)
            {
                ActionManager.Instance.ApplyStatusEffect(Owner, new PowerStatusEffect());
            }
        });
    }
}
