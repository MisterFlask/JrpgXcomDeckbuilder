using UnityEngine;
using System.Collections;

public class IneptShot : AbstractCard
{
    public IneptShot()
    {
        SetCommonCardAttributes("Inept Shot", Rarity.BASIC, TargetType.ENEMY, CardType.AttackCard, baseEnergyCost: 1, protoGameSprite: ProtoGameSprite.FromGameIcon("Sprites/bowman-sad"));
        BaseDamage = 4;
    }

    public override int BaseEnergyCost()
    {
        return 1;
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
