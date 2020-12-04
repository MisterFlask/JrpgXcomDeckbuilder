using UnityEngine;
using System.Collections;

public class Recklessness : AbstractCard
{
    public Recklessness()
    {
        Name = "Recklessness";
        BaseDamage = 8;
        CardType = CardType.AttackCard;
        TargetType = TargetType.ENEMY;
    }

    public override string Description()
    {
        return $"Deal {DisplayedDamage()} damage.  Gain 2 Power and lose 2 Dexterity.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        ActionManager.Instance.AttackUnitForDamage(target, Owner, BaseDamage);
        ActionManager.Instance.ApplyStatusEffect(Owner, new PowerStatusEffect(), 2);
        ActionManager.Instance.ApplyStatusEffect(Owner, new DexterityStatusEffect(), -2);
    }
}
