using UnityEngine;
using System.Collections;

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

    public override string Description()
    {
        return $"Deal {DisplayedDamage()} damage. Advance.  Add a Flanking Shot to your hand.";
    }

    public override int BaseEnergyCost()
    {
        return 1;
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        ActionManager.Instance.AttackUnitForDamage(target, this.Owner, BaseDamage);
        if (!target.IsAdvanced)
        {
            ActionManager.Instance.Advance(Owner);
        }
        ActionManager.Instance.AddCardToHand(new FlankingShot());
    }
}
