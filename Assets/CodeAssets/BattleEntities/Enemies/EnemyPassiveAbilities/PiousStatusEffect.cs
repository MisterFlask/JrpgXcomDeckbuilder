﻿using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class PiousStatusEffect : AbstractStatusEffect
    {
        // it costs 1 more energy to target this character with a card.
        public override string Description => "It costs 1 more energy to target this character with a card.";

        public override int GetTargetedCostModifier(AbstractCard card)
        {
            return 1;
        }
    }
}