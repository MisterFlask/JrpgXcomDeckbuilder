﻿using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses
{
    public class HammerSoldierClass : AbstractSoldierClass
    {
        public HammerSoldierClass()
        {
            EmblemIcon = ProtoGameSprite.EmblemIcon("hammer-break");
        }
        public override string Name()
        {
            return "Hammer";
        }
    }
}