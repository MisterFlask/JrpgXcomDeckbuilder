using UnityEngine;
using System.Collections;

public class Aggression : AbstractStatusEffect
{
    public override int DamageDealtAddition()
    {
        return Stacks;
    }

    public override int DefenseReceivedAddition()
    {
        return -1 * Stacks;
    }

    public override string Description => "Grants +1 Power and -1 Dexterity per stack.";
}
