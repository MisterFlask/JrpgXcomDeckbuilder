using UnityEngine;
using System.Collections;

public class IndiscriminateFire : AbstractCard
{
    public IndiscriminateFire()
    {
        BaseDamage = 5;
        SetCommonCardAttributes(
            "Indiscriminate Flames", 
            Rarity.UNCOMMON, 
            TargetType.NO_TARGET_OR_SELF, 
            CardType.AttackCard,
            1);
    }

    public override string Description()
    {
        return $"Deal {BaseDamage} damage to ALL enemies; deal 2 damage to ALL allies.";
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
