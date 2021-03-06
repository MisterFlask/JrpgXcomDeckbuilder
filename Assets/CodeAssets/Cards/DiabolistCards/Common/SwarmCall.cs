﻿using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.DiabolistCards.Special;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Common
{
    public class SwarmCall : AbstractCard
    {
        //  Cost 0. Gain 4 defense.  
        // A random Swarm card in your hand costs 1 less to play.
        // If there is none, create a Hellish Swarm in your draw pile.

        public SwarmCall()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Swarm Barrier", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);
            BaseDefenseValue = 2; 

        }

        public override string DescriptionInner()
        {
            return "A random Swarm card in your hand costs 1 less to play this combat.  If there is none, create a Hellish Swarm in your draw pile.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            var randomCard = BattleHelpers.RandomCardInHandThat(item => item.CardTags.Contains(BattleCardTags.SWARM) && item.GetDisplayedEnergyCost() > 0);
            if (randomCard == null)
            {
                action().CreateCardToBattleDeckDrawPile(new HellishSwarm(), CardCreationLocation.SHUFFLE);
            }
            else
            {
                randomCard.StaticBaseEnergyCost --;
            }
        }
    }
}