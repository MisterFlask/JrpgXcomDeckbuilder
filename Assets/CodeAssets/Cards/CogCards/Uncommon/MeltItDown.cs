﻿using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Common
{
    public class MeltItDown : AbstractCard
    {
        public MeltItDown()
        {
            SetCommonCardAttributes("Melt It Down", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);
        }

        // Technocannibalize:  Gain 1 energy and 1 Empowered.
        // Inferno: Then do it one more time.
        // Cost 0.
        public override string DescriptionInner()
        {
            return $"Technocannibalize:  Gain 1 energy and apply 1 Empowered to target.  Inferno: Then do it one more time.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            CardAbilityProcs.Technocannibalize(this, () =>
            {
                CardAbilityProcs.GainEnergy(this, 1);
                action().ApplyStatusEffect(target, new EmpoweredStatusEffect());
            });
            CardAbilityProcs.Inferno(this, () =>
            {
                CardAbilityProcs.GainEnergy(this, 1);
                action().ApplyStatusEffect(target, new EmpoweredStatusEffect());
            });
        }
    }
}