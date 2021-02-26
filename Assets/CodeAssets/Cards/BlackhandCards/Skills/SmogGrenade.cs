using UnityEngine;
using System.Collections;

/// <summary>
/// Applies Weak to all enemies.
/// </summary>
public class  SmogGrenade : AbstractCard
{
    public SmogGrenade()
    {
        SetCommonCardAttributes("Smog Grenade", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);        
    }

    public override string Description()
    {
        return $"Apply 1 Weak to all enemies";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        foreach(var character in state().EnemyUnitsInBattle)
        {
            action().ApplyStatusEffect(character, new WeakenedStatusEffect(), 1);
        }
    }
}
