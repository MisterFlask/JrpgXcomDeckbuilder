﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.Efficiency
{

    // defends twice, then flees.  This increases the Doom Counter by 2.
    public class EfficiencyAlarmDrone : AbstractEnemyUnit
    {
        public EfficiencyAlarmDrone()
        {
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/Proselytizer");
            this.Description = "???";
            this.CharacterNicknameOrEnemyName = "Alarm Drone";
            this.MaxHp = 22;
            this.EnemyFaction = EnemyFaction.EFFICIENCY;
            UnitSize = UnitSize.MEDIUM;
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromPercentBase.AttackRandomPc(
                    this,
                    50,
                    1),
                IntentsFromPercentBase.AttackRandomPc(
                    this,
                    50,
                    1),
                IntentsFromPercentBase.DefendSelf(
                    this,
                    50),
                IntentsFromPercentBase.DoMagic(this, () =>
                {
                    ActionManager.Instance.IncrementDoomCounter(2);
                    ActionManager.Instance.KillUnit(this);
                })
            );
        }
    }
}