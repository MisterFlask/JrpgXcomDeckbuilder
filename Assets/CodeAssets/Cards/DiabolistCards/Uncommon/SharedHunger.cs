﻿using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Uncommon
{
    public class SharedHunger : AbstractCard
    {
        // Power:
        // After you play a card with "blood" in the name, Exhaust it and ALL characters heal 2.  Leadership: Heal 4 instead.
        public override string DescriptionInner()
        {
            return $"After you play a card with 'blood' in the name, exhaust it and ALL allies heal 2";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(Owner, new SharedHungerStatusEffect(), 2);
        }
    }

    public class SharedHungerStatusEffect: AbstractStatusEffect
    {
        public SharedHungerStatusEffect()
        {
            Name = "Shared Hunger Power";
        }

        public override string Description => $"After you play a card with \"blood\" in the name, Exhaust it and ALL allies heal {Stacks}.";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard)
        {
            if (cardPlayed.NameContains("blood"))
            {
                foreach(var ally in state().AllyUnitsInBattle)
                {
                    action().HealUnit(ally, Stacks, this.OwnerUnit);
                }
            }
        }
    }
}