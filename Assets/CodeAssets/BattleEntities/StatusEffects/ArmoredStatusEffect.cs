using UnityEngine;
using System.Collections;

public class ArmoredStatusEffect : AbstractStatusEffect
{
    public ArmoredStatusEffect()
    {
        this.Name = "Armored";
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/fast-forward-button");
    }

    public override string Description => "Decreases attack damage taken by the number of stacks.  Ignored by Precision.";

    public override int DamageReceivedAddition()
    {
        return -1 * Stacks;
    }
}
