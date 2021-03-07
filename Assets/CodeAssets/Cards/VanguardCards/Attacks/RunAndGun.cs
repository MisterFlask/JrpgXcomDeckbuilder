using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class RunAndGun : AbstractCard
{
    public RunAndGun()
    {
        BaseDamage = 5;
        Name = "Run and Gun";
        CardType = CardType.AttackCard;
        TargetType = TargetType.ENEMY;
    }

    public override string DescriptionInner()
    {
        return $"Deal {DisplayedDamage()} damage.  If Advanced, removes Advanced.  Otherwise, gain Advanced.";
    }

    public override int BaseEnergyCost()
    {
        return 1;
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
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
