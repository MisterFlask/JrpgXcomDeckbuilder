using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Special
{
    public class Crusher : AbstractCard
    {
        // Deal 4 damage.  Precision.  Exhaust.  Cost 0.

        public Crusher()
        {
            SetCommonCardAttributes("Crusher", Rarity.NOT_IN_POOL, TargetType.ENEMY, CardType.AttackCard, 0);
            Stickers.Add(new BasicAttackTargetSticker());
            Stickers.Add(new BasicDefendSelfSticker());
            BaseDamage = 6;
            BaseDefenseValue = 4;
            DamageModifiers.Add(new SweeperDamageModifier());
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