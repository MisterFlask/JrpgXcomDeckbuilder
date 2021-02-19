using UnityEngine;
using System.Collections;

public class MovingTarget : AbstractCard
{
    public MovingTarget()
    {
        SoldierClassCardPools.Add(typeof(VanguardSoldierClass));
        SetCommonCardAttributes("Moving Target", Rarity.BASIC, TargetType.ENEMY, CardType.AttackCard, 1);
    }
    public override string Description()
    {
        return "if not advanced, apply advanced.  Otherwise, remove advanced.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
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
