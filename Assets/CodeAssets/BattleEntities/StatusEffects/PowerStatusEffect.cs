using UnityEngine;
using System.Collections;

public class PowerStatusEffect : AbstractStatusEffect
{
    public PowerStatusEffect()
    {
        this.Name = "Power";
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/weight-lifting-up");
        this.Stackable = true;
        this.AllowedToGoNegative = true;
    }

    public override string Description => $"Increases damage by 1 per stack.";

    public override int DamageDealtAddition()
    {
        return Stacks;
    }
}
