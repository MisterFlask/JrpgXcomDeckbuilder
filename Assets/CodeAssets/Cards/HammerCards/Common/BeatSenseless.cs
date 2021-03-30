using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards.Common
{
    public class BeatSenseless : AbstractCard
    {
        // Deal 10 damage and 2 Vulnerable.  Cost 2.
        // Brute: Gain 1 energy.
        public BeatSenseless()
        {
            SetCommonCardAttributes("Beat Senseless", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 2);
            BaseDamage = 10;
        }

        public override string DescriptionInner()
        {
            return $"Deal 10 damage and 2 Vulnerable.  Brute: Gain 1 energy.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            Action_ApplyStatusEffectToTarget(new VulnerableStatusEffect(), 2, target);
            CardAbilityProcs.Brute(this, () =>
            {
                CardAbilityProcs.Refund(this, 1);
            });
        }
    }
}