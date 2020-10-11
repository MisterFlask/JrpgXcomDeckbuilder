using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 *   30 HP.   Attacks:  2x3.
 *   TODO: a Flower debuff that goes on cards in your deck which causes them to spawn a Greywing when played.
 *   That would be pretty sweet.
 *   (Though I have not yet created card-specific effects.)
 */
public class Greywing : AbstractEnemyUnit
{
    public Greywing()
    {
        this.MaxHp = 30;
        this.AddStatusEffect(new GreywingWoundOnDeath(), stacks: 4);
    }

    public override List<Intent> GetNextIntents()
    {
        return SingleUnitAttackIntent.AttackRandomEnemy(this, 3, 2).ToSingletonList<Intent>();
    }
}


/// <summary>
///  When he dies, he applies four Wounded to the unit attacking.
/// </summary>
public class GreywingWoundOnDeath: AbstractStatusEffect
{
    public override string Description => $"This applies ${Stacks} Wounded to whoever killed it.";

    public override void OnDeath(AbstractBattleUnit unitThatKilledMe)
    {
        if (unitThatKilledMe != null)
        {
            unitThatKilledMe.AddStatusEffect(new Wounded(), stacks: Stacks);
        }
    }
}
