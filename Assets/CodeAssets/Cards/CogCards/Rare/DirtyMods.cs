﻿using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Rare
{
    public class DirtyMods : AbstractCard
    {
        // Add 2 Ontological Waste to your discard pile
        // ALL cards in your hand gain "Then, deal 5 damage to a random target."

        public DirtyMods()
        {
            SetCommonCardAttributes("Dirty Mods", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);

        }

        public override string DescriptionInner()
        {
            return "ALL cards in your hand gain 'Then, deal 5 damage to a random enemy.'  Create an Ontological Waste in your discard pile.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach(var cardInHand in state().Deck.Hand)
            {
                cardInHand.AddSticker(new BasicAttackRandomEnemySticker());
            }
            action().CreateCardToBattleDeckDiscardPile(new OntologicalWaste());
        }
    }
}