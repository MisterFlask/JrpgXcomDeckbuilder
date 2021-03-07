﻿using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Common
{
    public class MountingAdvantages : AbstractCard
    {
        public MountingAdvantages()
        {
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            this.SetCommonCardAttributes(
                "Mounting Advantages",
                Rarity.UNCOMMON,
                TargetType.ALLY,
                CardType.PowerCard,
                2
                );
            this.BaseDamage = 5;
        }

        public override string DescriptionInner()
        {
            return $"OTHER allies gain +1 strength per turn.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new AdvancedStatusEffect(), 1);
        }
    }
}