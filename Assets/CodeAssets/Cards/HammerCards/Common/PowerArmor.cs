using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards.Common
{
    public class PowerArmor : AbstractCard
    {
        // Gain 3 Empowered.  Cost 1.

        public PowerArmor()
        {
            SetCommonCardAttributes("Power Armor", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
        }

        public override string DescriptionInner()
        {
            return $"Gain 3 Empowered.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToOwner(new EmpoweredStatusEffect(), 3);
        }
    }
}