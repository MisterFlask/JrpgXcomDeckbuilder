using UnityEngine;
using System.Collections;

public class ThornsStatusEffect : AbstractStatusEffect
{
    public ThornsStatusEffect()
    {
        Name = "Thorns";
    }

    public override void OnStruck(AbstractBattleUnit unitStriking, int totalDamageTaken)
    {
        action().DamageUnitNonAttack(unitStriking, null, Stacks);
    }

    public override string Description => "Deals damage to attackers equal to number of stacks";
}
