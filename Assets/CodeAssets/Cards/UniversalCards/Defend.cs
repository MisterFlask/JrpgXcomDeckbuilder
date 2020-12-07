using UnityEngine;
using System.Collections;

public class Defend : AbstractCard
{
    public Defend()
    {
        this.BaseDefenseValue = 5;
        this.SetCommonCardAttributes("Defend", Rarity.BASIC, TargetType.ALLY, CardType.SkillCard, 1, protoGameSprite: ProtoGameSprite.FromGameIcon("Sprites/shield-reflect"));

    }


    public override string Description()
    {
        return $"Applies {DisplayedDefense()} defense to target ally." ;
    }
    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().ApplyDefense(target, this.Owner, this.BaseDefenseValue);
    }
}
