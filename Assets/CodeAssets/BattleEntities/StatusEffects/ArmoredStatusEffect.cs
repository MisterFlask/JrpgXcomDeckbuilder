using UnityEngine;
using System.Collections;

public class ArmoredStatusEffect : AbstractStatusEffect
{
    public ArmoredStatusEffect()
    {
        this.Name = "Armored";
        this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("abdominal-armor");
    }

    public override string Description => "Decreases attack damage taken by the number of stacks.  Ignored by Precision.";

    public override int DamageReceivedAddition()
    {
        return -1 * Stacks;
    }
}
