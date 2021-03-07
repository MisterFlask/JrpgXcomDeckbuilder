using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards
{
    public class GiveGround : AbstractCard
    {
        public GiveGround()
        {
            this.SetCommonCardAttributes(
                "You there!  Charge!",
                Rarity.COMMON,
                TargetType.ALLY,
                CardType.SkillCard,
                1
                );
        }

        public override string DescriptionInner()
        {
            return "Advance an ally.  Apply 2 Temporary Strength to that ally.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new AdvancedStatusEffect());
        }
    }
}