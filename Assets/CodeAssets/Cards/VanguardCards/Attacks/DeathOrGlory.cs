using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class DeathOrGlory : AbstractCard
{
    public DeathOrGlory()
    {
        SoldierClassCardPools.Add(typeof(VanguardSoldierClass));
        this.BaseDamage = 20;
        this.Name = "Death or Glory";
        CardType = CardType.AttackCard;
        TargetType = TargetType.ENEMY;
    }

    public override string DescriptionInner()
    {
        return $"Deal {DisplayedDamage()} damage.  Take {DisplayedDamage()/4} (1/4) damage.";
    }
    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        ActionManager.Instance.AttackUnitForDamage(target, Owner, BaseDamage, this);
        ActionManager.Instance.AttackUnitForDamage(Owner, Owner, BaseDamage / 4, this);

    }
}
