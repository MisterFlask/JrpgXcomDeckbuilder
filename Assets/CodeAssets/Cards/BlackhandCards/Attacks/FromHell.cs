using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Attacks
{
    public class FromHell : AbstractCard
    {
        public FromHell()
        {
            SetCommonCardAttributes("From Hell", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 0);
            this.BaseDamage = 10;
        }

        public override string DescriptionInner()
        {
            return $"Deal {BaseDamage} damage.  Inferno: This card deals 8 more damage for the rest of combat.";
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