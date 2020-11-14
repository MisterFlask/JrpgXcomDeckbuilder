using UnityEngine;
using System.Collections;

public class Recklessness : AbstractCard
{
    public Recklessness()
    {
        Name = "Recklessness";
    }

    public override string Description()
    {
        return $"Deal {DisplayedDamage()} damage.  Gain 2 Aggression.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        ActionManager.Instance.AttackUnitForDamage(target, Owner, BaseDamage);
        ActionManager.Instance.ApplyStatusEffect(Owner, new Aggression(), 2);
    }
}
