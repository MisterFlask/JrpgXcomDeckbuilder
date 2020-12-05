using UnityEngine;
using System.Collections;

public class IndiscriminateFlames : AbstractCard
{
    public IndiscriminateFlames()
    {
        SetCommonCardAttributes("Indiscriminate Flames", 
            Rarity.UNCOMMON, 
            TargetType.NO_TARGET_OR_SELF, 
            CardType.AttackCard, 1);
    }

    public override string Description()
    {
        return $"Deal {BaseDamage} damage to enemies; deal 1 damage to allies.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        foreach(var ally in state().AllyUnitsInBattle)
        {
            action().DamageUnitNonAttack(ally, Owner, 1);
        }
        foreach(var enemy in state().EnemyUnitsInBattle)
        {
            action().AttackUnitForDamage(enemy, Owner, BaseDamage);
        }
    }
}
