using UnityEngine;
using System.Collections;

public class RunAndGun : AbstractCard
{
    public RunAndGun()
    {
        Name = "Run and Gun";
    }

    public override string Description()
    {
        return $"Deal {DisplayedDamage()} damage.  If Advanced, removes Advanced.  Otherwise, gain Advanced.";
    }

    public override int BaseEnergyCost()
    {
        return 1;
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        ActionManager.Instance.AttackUnitForDamage(target, this.Owner, BaseDamage);
        ActionManager.Instance.DoAThing(() =>
        {
            if (target.IsAdvanced)
            {
                ActionManager.Instance.RemoveStatusEffect<AdvancedStatusEffect>(Owner);
            }
            else
            {
                ActionManager.Instance.Advance(Owner);
            }
        });
    }
}
