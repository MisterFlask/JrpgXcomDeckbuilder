using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class Recklessness : AbstractCard
{
    public Recklessness()
    {
        SoldierClassCardPools.Add(typeof(VanguardSoldierClass));
        Name = "Recklessness";
        BaseDamage = 8;
        CardType = CardType.AttackCard;
        TargetType = TargetType.ENEMY;
        Rarity = Rarity.UNCOMMON;
    }

    public override string DescriptionInner()
    {
        return $"Deal {DisplayedDamage()} damage.  Gain 2 Power and lose 2 Dexterity.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        ActionManager.Instance.AttackUnitForDamage(target, Owner, BaseDamage);
        ActionManager.Instance.ApplyStatusEffect(Owner, new PowerStatusEffect(), 2);
        ActionManager.Instance.ApplyStatusEffect(Owner, new DexterityStatusEffect(), -2);
    }
}
