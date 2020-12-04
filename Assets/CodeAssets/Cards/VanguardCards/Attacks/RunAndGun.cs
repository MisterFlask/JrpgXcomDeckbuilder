using UnityEngine;
using System.Collections;

public class RunAndGun : AbstractCard
{
    public RunAndGun()
    {
        BaseDamage = 5;
        Name = "Run and Gun";
        CardType = CardType.AttackCard;
        TargetType = TargetType.ENEMY;
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
        if (target.IsAdvanced)
        {
            ActionManager.Instance.RemoveStatusEffect<AdvancedStatusEffect>(Owner);
        }
        else
        {
            ActionManager.Instance.Advance(Owner);
        }
    }
}
