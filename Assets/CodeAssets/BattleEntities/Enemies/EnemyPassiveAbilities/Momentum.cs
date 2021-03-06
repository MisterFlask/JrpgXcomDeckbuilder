﻿using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class Momentum : AbstractStatusEffect
    {
        public Momentum()
        {
            Name = "Momentum";
        }

        public override string Description => "Each turn, gain [Stacks] strength.";

        public override void OnTurnStart()
        {
            ActionManager.Instance.ApplyStatusEffect(OwnerUnit, new StrengthStatusEffect(), Stacks);
        }
    }
}