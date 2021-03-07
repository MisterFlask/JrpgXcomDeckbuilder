using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class MovingTarget : AbstractCard
{
    public MovingTarget()
    {
        SoldierClassCardPools.Add(typeof(VanguardSoldierClass));
        SetCommonCardAttributes("Moving Target", Rarity.BASIC, TargetType.ENEMY, CardType.AttackCard, 1);
    }
    public override string DescriptionInner()
    {
        return "if not advanced, apply advanced.  Otherwise, remove advanced.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
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
