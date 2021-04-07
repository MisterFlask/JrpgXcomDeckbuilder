using Assets.CodeAssets.Cards.CogCards.Special;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Common
{
    public class DroneController : AbstractCard
    {
        // add a Shield Drone to your hand.  Energized: Add another.  Cost 1.
        public DroneController()
        {
            SetCommonCardAttributes("Drone Controller", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);
        }

        public override string DescriptionInner()
        {
            return "Add a Shield Drone to your hand.  Energized: Add another.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().CreateCardToHand(new ShieldDrone());
            CardAbilityProcs.Energized(this, () =>
            {
                action().CreateCardToHand(new ShieldDrone());
            });
        }
    }
}