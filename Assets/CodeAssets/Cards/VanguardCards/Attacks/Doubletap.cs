using UnityEngine;
using System.Collections;

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

    public override string Description()
    {
        return $"Deal {DisplayedDamage()}, twice.  Slay: Gain 1 Power.";
    }
    protected override void OnPlay(AbstractBattleUnit target)
    {
        ActionManager.Instance.AttackUnitForDamage(target, Owner, BaseDamage);
        if (target.IsDead)
        {
            ActionManager.Instance.ApplyStatusEffect(Owner, new PowerStatusEffect(), 1);
        }
    }
}
