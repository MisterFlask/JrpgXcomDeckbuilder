using UnityEngine;
using System.Collections;

// increases defense, increases stress
public class Smokescreen : AbstractCard
{
    public Smokescreen()
    {
        SetCommonCardAttributes("Smokescreen", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
        this.BaseDefenseValue = 8;
    }

    public override string Description()
    {
        return $"Apply {DisplayedDefense()} to all allied units.  All Ally units gain 2 Stress.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        foreach (var character in state().AllyUnitsInBattle)
        {
            action().ApplyDefense(character, Owner, BaseDefenseValue);
            action().ApplyStatusEffect(target, new StressStatusEffect(), 2);
        }
    }
}
