using UnityEngine;
using System.Collections;

public class FumesLeak : AbstractCard
{
    public FumesLeak()
    {
        SetCommonCardAttributes("ApolloQik Leak", Rarity.COMMON, TargetType.ENEMY, CardType.SkillCard, 1);
        BaseDefenseValue = 4;
    }

    public override string Description()
    {
        return $"Adds 8 Flammable to targeted enemy.  Apply {BaseDefenseValue} defense and 4 Stress to all allies.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().ApplyStatusEffect(target, new FlammableStatusEffect(), 8);
        foreach(var ally in state().AllyUnitsInBattle)
        {
            action().ApplyDefense(ally, Owner, BaseDefenseValue);
            action().ApplyStatusEffect(ally, new StressStatusEffect(), 4);
        }
    }
}
