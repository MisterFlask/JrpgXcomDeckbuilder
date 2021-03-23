using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards
{
    public class GiveGround : AbstractCard
    {
        public GiveGround()
        {
            this.SetCommonCardAttributes(
                "Give ground",
                Rarity.BASIC,
                TargetType.ALLY,
                CardType.SkillCard,
                1
                );
        }

        public override string DescriptionInner()
        {
            return "Apply Advanced to an ally.  That ally gains +1 Temporary Strength.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new AdvancedStatusEffect());
            action().ApplyStatusEffect(target, new TemporaryStrengthStatusEffect());
        }
    }
}