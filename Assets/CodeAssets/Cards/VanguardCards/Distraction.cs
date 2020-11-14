using UnityEngine;
using System.Collections;

public class Distraction : AbstractCard
{
    public Distraction()
    {
        Name = "Distraction";
        BaseDefenseValue = 4;
    }

    public override string Description()
    {
        return $"Apply {DisplayedDefense()} to every other ally";
    }

    protected override void OnPlay(AbstractBattleUnit target)
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
