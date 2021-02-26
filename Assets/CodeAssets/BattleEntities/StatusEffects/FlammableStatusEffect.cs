using UnityEngine;
using System.Collections;

public class FlammableStatusEffect : AbstractStatusEffect
{
    public FlammableStatusEffect()
    {
        Name = "Flammable";
        ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/gas-stove", Color.yellow);
    }
    public override string Description => "Ticks down by half its stacks per turn.  " +
        "If Fuel is applied, removes all Flammable and adds that much Burning.";

    public override void OnTurnStart()
    {
        action().ApplyStatusEffect(this.OwnerUnit, new FlammableStatusEffect(), -1 * Stacks / 2);
    }
}
