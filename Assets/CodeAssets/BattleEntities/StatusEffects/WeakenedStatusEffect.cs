using UnityEngine;
using System.Collections;

public class WeakenedStatusEffect : AbstractStatusEffect
{
    public WeakenedStatusEffect()
    {
        this.Name = "Weak";
    }

    public override string Description => "Reduces damage by 1/3.  On stack is removed per turn.";

    public override float DamageDealtMultiplier()
    {
        return .666f;
    }
}
