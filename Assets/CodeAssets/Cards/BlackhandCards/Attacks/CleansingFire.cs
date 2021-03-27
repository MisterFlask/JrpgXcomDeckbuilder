using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Attacks
{
    public class CleansingFire : AbstractCard
    {
        /// Deal 10 damage.  Lethal: Relieve 8 stress.
        
        public CleansingFire()
        {
            SetCommonCardAttributes("Cleansing Fire", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 2);
            this.DamageModifiers.Add(new SweeperDamageModifier());
            this.DamageModifiers.Add(new LethalTriggerDamageModifier("Relieve 8 stress.", (killedEnemy) =>
            {
                action().ApplyStress(this.Owner, -8);
            }));
            this.BaseDamage = 8;
        }

        public override string DescriptionInner()
        {
            return "Deal 8 damage.  Lethal: Relieve 8 stress.  Sweeper.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().AttackWithCard(this, target);
        }
    }
}