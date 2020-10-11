using UnityEngine;
using System.Collections;

public class Wounded : AbstractStatusEffect
{
    public Wounded()
    {
        this.Name = "Wounded";
        this.Stackable = true;
    }

    public override string Description => $"This unit takes {Stacks} extra damage from attacks. Decreases by 1 each turn.";

    public override int DamageReceivedAddition()
    {
        return 1 * Stacks;
    }

}
