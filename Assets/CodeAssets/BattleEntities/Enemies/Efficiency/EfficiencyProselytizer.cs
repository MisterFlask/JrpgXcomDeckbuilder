﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.Efficiency
{
    public class EfficiencyProselytizer : AbstractEnemyUnit
    {
        public EfficiencyProselytizer()
        {
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/RoboVAK", color: Color.red);
            this.Description = "???";
            this.CharacterNicknameOrEnemyName = "Proselytizer";
            this.EnemyFaction = EnemyFaction.EFFICIENCY;
            this.ApplyStatusEffect(new ArmoredStatusEffect(), stacks: 4);
        }

        //Increase stress of all characters by 10 => attack for 20%

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
//                IntentsFromPercentBase.Charging(this),
//                IntentsFromPercentBase.Charging(this),
                IntentsFromPercentBase.AttackRandomPcWithCardToDiscardPile(
                    new TargetingReticle(),
                    this,
                    200,
                    1),
                IntentsFromPercentBase.DefendSelf(this, 50));
        }
    }
}