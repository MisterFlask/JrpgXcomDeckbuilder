﻿using UnityEngine;
using System.Collections;

public class AdvancedStatusEffect : AbstractStatusEffect
{
    public AdvancedStatusEffect()
    {
        this.Name = "Advanced";
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/fast-forward-button");
        this.Stackable = false;
    }

    public override string Description => "Doubles damage both dealt and received.";

    public override float DamageReceivedIncrementalMultiplier()
    {
        return 2f;
    }

    public override float DamageDealtIncrementalMultiplier()
    {
        return 2f;
    }
}
