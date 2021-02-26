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

        public override string Description()
        {
            return "Advance an ally.  Apply 2 Temporary Strength to that ally.";
        }

        protected override void OnPlay(AbstractBattleUnit target)
        {
            action().ApplyStatusEffect(target, new AdvancedStatusEffect());
        }
    }
}