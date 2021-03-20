using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class Withdraw : AbstractCard
{
    public Withdraw()
    {
        Name = "Distraction";
        BaseDefenseValue = 4;
        CardType = CardType.SkillCard;
        TargetType = TargetType.ALLY;
    }

    public override string DescriptionInner()
    {
        return $"Apply {DisplayedDefense()} to an ally.  {ownerDisplayString()} loses Advanced.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        ActionManager.Instance.ApplyDefense(target, Owner, BaseDefenseValue);
        ActionManager.Instance.RemoveStatusEffect<AdvancedStatusEffect>(Owner);
    }

}
