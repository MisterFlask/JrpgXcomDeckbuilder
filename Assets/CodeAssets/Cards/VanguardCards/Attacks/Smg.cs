using UnityEngine;
using System.Collections;

public class Smg : AbstractCard
{
    public Smg()
    {
        this.Rarity = Rarity.NOT_IN_CARD_POOL;
        this.BaseDamage = 4;
        this.Name = "SMG";
        CardType = CardType.AttackCard;
        TargetType = TargetType.ENEMY;
    }

    public override string Description()
    {
        return $"Deal {DisplayedDamage()} damage, twice.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        ActionManager.Instance.AttackUnitForDamage(target, Owner, BaseDamage);
        ActionManager.Instance.AttackUnitForDamage(target, Owner, BaseDamage);
    }
}
