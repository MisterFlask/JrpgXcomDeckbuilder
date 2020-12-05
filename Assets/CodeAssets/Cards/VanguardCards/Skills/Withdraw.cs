using UnityEngine;
using System.Collections;

public class Withdraw : AbstractCard
{
    public Withdraw()
    {
        Name = "Distraction";
        BaseDefenseValue = 4;
        CardType = CardType.SkillCard;
        TargetType = TargetType.ALLY;
    }

    public override string Description()
    {
        return $"Apply {DisplayedDefense()} to an ally.  {OwnerDisplayName()} loses Advanced.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        ActionManager.Instance.ApplyDefense(target, Owner, BaseDefenseValue);
        ActionManager.Instance.RemoveStatusEffect<AdvancedStatusEffect>(Owner);
    }

}
