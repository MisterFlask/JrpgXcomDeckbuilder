using UnityEngine;
using System.Collections;

// todo: Do we actually want this?  Maybe call it "technique"?
public class DexterityStatusEffect : AbstractStatusEffect
{
    public DexterityStatusEffect()
    {
        this.Name = "Technique";
        ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/leapfrog", Color.cyan);
        this.Stackable = true;
        this.AllowedToGoNegative = true;
    }

    public override string Description => $"Whenever you apply defense, apply [Stacks] more.";

    public override int DefenseReceivedAddition()
    {
        return Stacks;
    }
}
