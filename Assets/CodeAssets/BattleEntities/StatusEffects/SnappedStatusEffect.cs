using UnityEngine;
using System.Collections;

public class SnappedStatusEffect : AbstractStatusEffect
{
    public SnappedStatusEffect()
    {
        Name = "Snapped";
        Stackable = false;
        this.ProtoSprite = GameIconProtoSprite.FromGameIcon(path: "Sprites/propeller-beanie");
    }

    public override string Description => "if this character's Stress goes above their maximum HP value this battle, the character <color=red>dies</color>";
}
