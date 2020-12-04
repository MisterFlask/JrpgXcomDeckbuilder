using UnityEngine;
using System.Collections;

public class Defend : AbstractCard
{
    public Defend()
    {
        this.Name = "Defend";
        this.TargetType = TargetType.ALLY;
        this.CardType = CardType.SkillCard;
        this.BaseDefenseValue = 5;
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
