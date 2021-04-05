using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Special
{
    public class AutocannonSentry : AbstractCard
    {
        // Deal 4 damage.  Precision.  Exhaust.  Cost 0.

        public AutocannonSentry()
        {
            SetCommonCardAttributes("Autocannon Sentry", Rarity.NOT_IN_POOL, TargetType.ENEMY, CardType.AttackCard, 0);
            Stickers.Add(new BasicAttackTargetSticker());
            BaseDamage = 4;
            DamageModifiers.Add(new PrecisionDamageModifier());
        }

        public override string DescriptionInner()
        {
            return "";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
        }
    }
}