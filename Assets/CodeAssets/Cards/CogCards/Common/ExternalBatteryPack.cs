﻿using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Common
{
    public class ExternalBatteryPack : AbstractCard
    {
        // apply 3 Charged to an ally.

        public ExternalBatteryPack()
        {
            SetCommonCardAttributes("External Battery Pack", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 1);
        }

        public override string DescriptionInner()
        {
            return "Apply 3 Charged to an ally.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new ChargedStatusEffect(), 3);
        }
    }
}