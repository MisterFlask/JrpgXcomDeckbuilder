using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;
using Assets.CodeAssets.GameLogic;

public class Flamer : AbstractCard
{
    public Flamer()
    {
        this.SoldierClassCardPools.Add(typeof(Rarity));
        SetCommonCardAttributes("Flamer", Rarity.BASIC, TargetType.ENEMY, CardType.AttackCard, 0);
    }

    public override string DescriptionInner()
    {
        return $"Applies 3 Burning.  Increase the cost of this card by 1 and its Burning value by 2.  Whenever you play a Skill, Cycle this.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        ActionManager.Instance.ApplyStatusEffect(target, new BurningStatusEffect(), 3);
        this.EnergyCostMod += 1;
    }

}
