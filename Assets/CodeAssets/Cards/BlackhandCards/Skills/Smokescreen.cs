using UnityEngine;
using System.Collections;

// increases defense, increases stress
public class Smokescreen : AbstractCard
{
    public Smokescreen()
    {
        Name = "Smokescreen";
        TargetType = TargetType.NO_TARGET_OR_SELF;
        this.BaseDefenseValue = 4;
    }

    public override string Description()
    {
        return $"Apply {DisplayedDefense()} to all allied units.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        foreach (var character in state().AllyUnitsInBattle)
        {
            action().ApplyDefense(character, Owner, BaseDefenseValue);
        }

    }
}
