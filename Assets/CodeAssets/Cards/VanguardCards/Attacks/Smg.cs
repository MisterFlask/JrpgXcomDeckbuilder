using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class Smg : AbstractCard
{
    public Smg()
    {
        this.Rarity = Rarity.BASIC;
        this.BaseDamage = 4;
        this.Name = "SMG";
        CardType = CardType.AttackCard;
        TargetType = TargetType.ENEMY;
    }

    public override string DescriptionInner()
    {
        return $"Deal {DisplayedDamage()} damage, twice.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        ActionManager.Instance.AttackUnitForDamage(target, Owner, BaseDamage);
        ActionManager.Instance.AttackUnitForDamage(target, Owner, BaseDamage);
    }
}
