using UnityEngine;
using System.Collections;

public class FlammableStatusEffect : AbstractStatusEffect
{
    public FlammableStatusEffect()
    {
        Name = "Flammable";
        ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/gas-stove", Color.yellow);
    }
    public override string Description => "Ticks down by 1 stack per turn  If Fuel is applied, removes all Flammable and adds that much Burning.";
}
