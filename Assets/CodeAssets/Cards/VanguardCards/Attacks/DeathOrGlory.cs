using UnityEngine;
using System.Collections;

public class DeathOrGlory : AbstractCard
{
    public DeathOrGlory()
    {
        this.BaseDamage = 20;
        this.Name = "Death or Glory";
        CardType = CardType.AttackCard;
        TargetType = TargetType.ENEMY;
    }

    public override string Description()
    {
        return $"Deal {DisplayedDamage()} damage.  Take {DisplayedDamage()/4} (1/4) damage.";
    }
    protected override void OnPlay(AbstractBattleUnit target)
    {
        ActionManager.Instance.AttackUnitForDamage(target, Owner, BaseDamage);
        ActionManager.Instance.AttackUnitForDamage(Owner, Owner, BaseDamage/4);

    }
}
