using UnityEngine;
using System.Collections;

public class Flamer : AbstractCard
{
    public Flamer()
    {
        this.SoldierClassCardPools.Add(typeof(Rarity));
        SetCommonCardAttributes("Flamer", Rarity.BASIC, TargetType.ENEMY, CardType.AttackCard, 0);
    }

    public override string Description()
    {
        return $"Applies 3 Burning.  Increase the cost of this card by 1 and its Burning value by 2.  Whenever you play a Skill, Cycle this.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        ActionManager.Instance.ApplyStatusEffect(target, new BurningStatusEffect(), 3);
        this.EnergyCostMod += 1;
    }

}
