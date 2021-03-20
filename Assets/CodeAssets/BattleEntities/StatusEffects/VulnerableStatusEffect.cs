using UnityEngine;
using System.Collections;

public class VulnerableStatusEffect : AbstractStatusEffect
{
    public VulnerableStatusEffect()
    {
        Name = "Vulnerable";
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/shield-minus", Color.red);
    }
    
    public override string Description => $"increases damage received by 50%";

    public override float DamageReceivedIncrementalMultiplier()
    {
        return 1.5f;
    }
}
