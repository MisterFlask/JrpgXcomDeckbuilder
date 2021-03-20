using UnityEngine;
using System.Collections;

public class WeakenedStatusEffect : AbstractStatusEffect
{
    public WeakenedStatusEffect()
    {
        this.Name = "Weak";
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/swordman-minus", Color.red);
    }

    public override string Description => "Reduces damage by 1/3.  On stack is removed per turn.";

    public override float DamageDealtIncrementalMultiplier()
    {
        return .666f;
    }
}
