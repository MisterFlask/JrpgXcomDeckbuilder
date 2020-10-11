using UnityEngine;
using System.Collections;

public class PowerStatusEffect : AbstractStatusEffect
{
    public PowerStatusEffect()
    {
        this.Stackable = true;
    }

    public override string Description => $"Increases damage by {Stacks}";

    public override int DamageDealtAddition()
    {
        return Stacks;
    }
}
