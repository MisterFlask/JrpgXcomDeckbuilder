using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class Attack : AbstractCard
{
    public Attack()
    {
        BaseDamage = 6;
        this.SetCommonCardAttributes("Attack", Rarity.BASIC, TargetType.ENEMY, CardType.AttackCard, 1, protoGameSprite: ProtoGameSprite.FromGameIcon("Sprites/sword-wound"));
    }

    public override string DescriptionInner()
    {
        return $"Deal {DisplayedDamage()} damage";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage, this);
    }
}
