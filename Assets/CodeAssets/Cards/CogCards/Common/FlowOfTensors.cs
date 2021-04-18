using Assets.CodeAssets.Cards.CogCards.Special;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Common
{
    public class FlowOfTensors : AbstractCard
    {
        // Add two Autocannons to your hand.  Energized: Gain a data point and Exhaust.  Cost 1.

        public FlowOfTensors()
        {
            SetCommonCardAttributes("Flow of Tensors", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
        }

        public override string DescriptionInner()
        {
            return "Add two Autocannons to your hand.  Energized: Gain a data point and Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().CreateCardToHand(new AutocannonSentry());
            action().CreateCardToHand(new AutocannonSentry());

            CardAbilityProcs.Energized(this, () =>
            {
                CardAbilityProcs.GainDataPoints(this, 1);
                Action_Exhaust();
            });
        }
    }
}