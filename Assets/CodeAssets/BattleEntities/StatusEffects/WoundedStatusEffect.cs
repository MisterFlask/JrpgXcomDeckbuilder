using UnityEngine;
using System.Collections;

public class WoundedStatusEffect : AbstractStatusEffect
{
    public WoundedStatusEffect()
    {
        this.Name = "Wounded";
        this.Stackable = true;
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/bleeding-wound", Color.red);
    }

    public override string Description => $"This unit takes {Stacks} extra damage from attacks. Decreases by 1 each turn.";

    public override int DamageReceivedAddition()
    {
        return 1 * Stacks;
    }

}
