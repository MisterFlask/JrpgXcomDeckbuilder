using Assets.CodeAssets.Cards.ArchonCards.Effects;
using Assets.CodeAssets.GameLogic;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Uncommon
{
    public class BloodOffering : AbstractCard
    {
        // Cost 7.  Apply 20 Temporary HP to all allies.  Bloodprice.

        public BloodOffering()
        {
            SetCommonCardAttributes("Blood Offering", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 7);
        }

        public override string DescriptionInner()
        {
            return "Apply 20 temporary HP to all allies.  Bloodprice.  Exhaust.";
        }

        public override EnergyPaidInformation GetNetEnergyCost()
        {
            return BloodpriceBattleRules.GetNetEnergyCostWithBloodprice(this);
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach(var ally in state().AllyUnitsInBattle)
            {
                action().ApplyStatusEffect(ally, new TemporaryHpStatusEffect(), 20);
            }
            this.ExhaustAsAction();
        }
    }
}