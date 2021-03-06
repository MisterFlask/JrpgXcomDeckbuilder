﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicAllyUnit : AbstractBattleUnit
{
    public BasicAllyUnit()
    {
        this.MaxHp = 20;
        this.CurrentHp = 20;
        this.CharacterFullName = "Basic Ally";
        this.IsAlly = true;
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(color:Color.blue);
        this.IsAiControlled = false;
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        return new List<AbstractIntent>();
    }
}
