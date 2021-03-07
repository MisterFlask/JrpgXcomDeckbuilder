using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class Charge : AbstractCard
{

    public Charge()
    {
        SoldierClassCardPools.Add(typeof(VanguardSoldierClass));
        BaseDamage = 5;
        Name = "Charge";
        CardType = CardType.AttackCard;
        TargetType = TargetType.ENEMY;
    }

    public override string DescriptionInner()
    {
        return $"Deal {DisplayedDamage()} damage. Advance.  Add a Flanking Shot to your hand.";
    }

    public override int BaseEnergyCost()
    {
        return 1;
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        ActionManager.Instance.AttackUnitForDamage(target, this.Owner, BaseDamage);
        if (!target.IsAdvanced)
        {
            ActionManager.Instance.Advance(Owner);
        }
        ActionManager.Instance.AddCardToHand(new FlankingShot());
    }
}
