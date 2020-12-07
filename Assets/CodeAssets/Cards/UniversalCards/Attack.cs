using UnityEngine;
using System.Collections;

public class Attack : AbstractCard
{
    public Attack()
    {
        BaseDamage = 6;
        this.SetCommonCardAttributes("Attack", Rarity.BASIC, TargetType.ENEMY, CardType.AttackCard, 1, protoGameSprite: ProtoGameSprite.FromGameIcon("Sprites/sword-wound"));
    }

    public override string Description()
    {
        return $"Deal {DisplayedDamage()} damage";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage);
    }
}
