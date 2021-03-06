﻿using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Attacks
{
    public class FromHell : AbstractCard
    {
        public FromHell()
        {
            SetCommonCardAttributes("From Hell", Rarity.UNCOMMON, TargetType.ENEMY, CardType.AttackCard, 2);
            this.BaseDamage = 10;
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage.  Inferno: This card deals 8 more damage for the rest of combat.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().AttackWithCard(this, target);
            action().DoAThing( () =>
            {
                this.BaseDamage += 8;
            });
        }

    }
}