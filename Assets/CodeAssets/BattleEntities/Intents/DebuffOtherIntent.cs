using UnityEngine;
using System.Collections;
using System;

public class DebuffOtherIntent : SimpleIntent
{
    public DebuffOtherIntent(AbstractBattleUnit owner, Action onPerformance): base(owner, 
        ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/poison-gas", color: Color.green))
    {
    }

    public override string GetGenericDescription()
    {
        return "This enemy is about to apply a debuff to one or more of your soldiers.";
    }

    public override string GetOverlayText()
    {
        return ""; // no overlay text for this class of intent.
    }

    protected override void Execute()
    {
        
    }
}
