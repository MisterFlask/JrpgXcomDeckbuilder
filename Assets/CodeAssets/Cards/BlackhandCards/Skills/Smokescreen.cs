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
        return $"Apply 1 Evade to all allies.  All allies gain 3 Stress.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        foreach (var character in state().AllyUnitsInBattle)
        {
            action().ApplyDefense(character, Owner, BaseDefenseValue);
            action().ApplyStatusEffect(target, new StressStatusEffect(), 3);
        }
    }
}
