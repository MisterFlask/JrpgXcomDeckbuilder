﻿using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Rare
{
    public class EnvironmentallyIrresponsible : AbstractCard
    {
        // Gain 4 Dexterity.  Create two Ontological Waste in your discard pile.  Exhaust.  Cost 3.
        // Inferno: Then do it again.

        public EnvironmentallyIrresponsible()
        {
            SetCommonCardAttributes("Environmentally Irresponsible", Rarity.RARE, TargetType.ENEMY, CardType.SkillCard, 3);
        }

        public override string DescriptionInner()
        {
            return $"Gain 4 Dexterity.  Create two Ontological Waste in your discard pile.  Exhaust.  Inferno: Then do it again!";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {

        }
    }
}