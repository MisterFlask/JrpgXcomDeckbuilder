﻿
public class TemporaryThorns : AbstractStatusEffect
{
    public TemporaryThorns()
    {
        Name = "Temporary Thorns";
    }

    public override void OnStruck(AbstractBattleUnit unitStriking, int totalDamageTaken)
    {
        action().DamageUnitNonAttack(unitStriking, null, Stacks);
    }

    public override string Description => "Deals damage to attackers equal to number of stacks.  Goes away at start of next turn.";

    public override void OnTurnStart()
    {
        action().RemoveStatusEffect<TemporaryThorns>(OwnerUnit);
    }
}