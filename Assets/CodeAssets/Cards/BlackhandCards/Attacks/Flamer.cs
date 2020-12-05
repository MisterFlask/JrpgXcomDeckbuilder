using UnityEngine;
using System.Collections;

public class Flamer : AbstractCard
{
    public Flamer()
    {
        BaseTechValue = 5;
        SetCommonCardAttributes("Flamer", Rarity.BASIC, TargetType.ENEMY, CardType.AttackCard, 0);
    }

    public override string Description()
    {
        return $"Applies {TechValue} Burning.  Increase the cost of this card by 1.  Scales with Tech.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        this.EnergyCostMod += 1;
        ActionManager.Instance.ApplyStatusEffect(target, new BurningStatusEffect(), TechValue);
    }
}
