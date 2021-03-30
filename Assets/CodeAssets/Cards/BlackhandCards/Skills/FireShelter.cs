using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Skills
{
    public class FireShelter : AbstractCard
    {
        // Apply 4 Temporary Thorns and 20 defense to the target.
        // Cost 2.

        public FireShelter()
        {
            SetCommonCardAttributes("Fire Shelter", Rarity.UNCOMMON, TargetType.ALLY, CardType.SkillCard, 2, typeof(BlackhandSoldierClass));
        }

        public override string DescriptionInner()
        {
            return $"Apply 4 Temporary Thorns and {DisplayedDefense()} defense to the target";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new TemporaryThorns(), 4);
            action().ApplyDefense(target, this.Owner, BaseDefenseValue);
        }
    }
}