using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Powers
{
    public class WalkingGenerator : AbstractCard
    {
        // Apply 12 block to target ally.  Empowered: Refund 1 energy.  cost 1.

        public WalkingGenerator()
        {
            SetCommonCardAttributes("Walking Generator", Rarity.RARE, TargetType.ALLY, CardType.SkillCard, 1, typeof(BlackhandSoldierClass));
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} block to target ally.  Energized: Refund 1 energy.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyDefense(target, this.Owner, BaseDefenseValue);
            CardAbilityProcs.Energized(this, () =>
            {
                state().energy++;
            });
        }
    }
}