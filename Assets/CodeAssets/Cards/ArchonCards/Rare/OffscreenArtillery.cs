﻿using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Rare
{
    public class OffscreenArtillery : AbstractCard
    {
        public OffscreenArtillery()
        {
            DamageModifiers.Add(new BusterDamageModifier());
            SetCommonCardAttributes("Offscreen Artillery", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.AttackCard, 3);
            this.BaseDamage = 11;
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage to a random enemy 3 times.  Buster.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().AttackWithCard(this, CardTargeting.RandomTargetableEnemy());
            action().AttackWithCard(this, CardTargeting.RandomTargetableEnemy());
            action().AttackWithCard(this, CardTargeting.RandomTargetableEnemy());
        }
    }
}