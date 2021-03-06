﻿using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Starting
{
    public class GiveGround : AbstractCard
    {
        public GiveGround()
        {

            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass)); // todo: remove
            this.SetCommonCardAttributes(
                "Give Ground!",
                Rarity.BASIC,
                TargetType.ALLY,
                CardType.SkillCard,
                1
                );

            this.BaseDefenseValue = 5;
        }

        public override string DescriptionInner()
        {
            return $"Remove Advanced from an ally.  Apply {DisplayedDefense()} defense.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().RemoveStatusEffect<AdvancedStatusEffect>(target);
            action().ApplyDefense(target, Owner, BaseDefenseValue);
        }
    }
}