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
        this.MagicNumber = 3;
        BaseDamage = 1;
        Stickers.Add(new BasicAttackTargetSticker());
    }

    public override string DescriptionInner()
    {
        return $"Applies 3 Burning.  Increase the cost of this card by 1 and its Burning value by 2.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        ActionManager.Instance.ApplyStatusEffect(target, new BurningStatusEffect(), MagicNumber);
        this.PersistentCostModifiers.Add(new RestOfCombatCostModifier(1));
        this.MagicNumber += 2;
    }

}
