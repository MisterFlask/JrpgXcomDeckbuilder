using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class Distraction : AbstractCard
{
    public Distraction()
    {
        Name = "Distraction";
        BaseDefenseValue = 4;
        CardType = CardType.SkillCard;
        TargetType = TargetType.ALLY;
    }

    public override string DescriptionInner()
    {
        return $"Apply {DisplayedDefense()} to every OTHER ally.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        foreach (var character in GameState.Instance.AllyUnitsInBattle)
        {
            if (character != Owner)
            {
                ActionManager.Instance.ApplyDefense(character, Owner, BaseDefenseValue);
            }
        }
    }

}
