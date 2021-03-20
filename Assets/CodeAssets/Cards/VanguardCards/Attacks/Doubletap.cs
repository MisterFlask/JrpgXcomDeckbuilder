using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class Doubletap : AbstractCard
{
    public Doubletap()
    {
        SoldierClassCardPools.Add(typeof(VanguardSoldierClass));
        Name = "Doubletap";
        BaseDamage = 4;
        CardType = CardType.AttackCard;
        TargetType = TargetType.ENEMY;
    }

    public override string DescriptionInner()
    {
        return $"Deal {DisplayedDamage()}, twice.  Slay: Gain 1 Power.";
    }
    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        ActionManager.Instance.AttackUnitForDamage(target, Owner, BaseDamage, this);
        if (target.IsDead)
        {
            ActionManager.Instance.ApplyStatusEffect(Owner, new StrengthStatusEffect(), 1);
        }
    }
}
