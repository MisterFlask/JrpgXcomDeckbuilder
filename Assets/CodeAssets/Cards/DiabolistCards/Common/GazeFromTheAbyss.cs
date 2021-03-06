﻿using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using Assets.CodeAssets.GameLogic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Common
{
    public class GazeFromTheAbyss : AbstractCard
    {
        // Apply 20 Terror and 1 Vulnerable to an enemy.
        // cost 2
        public GazeFromTheAbyss()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Abyssal Gaze", Rarity.COMMON, TargetType.ENEMY, CardType.SkillCard, 2);
            this.BaseDefenseValue = 12;
        }

        public override EnergyPaidInformation GetNetEnergyCost()
        {
            return BloodpriceBattleRules.GetNetEnergyCostWithBloodprice(this);
        }

        public override string DescriptionInner()
        {
            return "Apply 20 Binding and 1 Vulnerable to the enemy.  Bloodprice.";
        }


        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new BindingStatusEffect(), 20);
            action().ApplyStatusEffect(target, new VulnerableStatusEffect(), 1);
        }

    }
}