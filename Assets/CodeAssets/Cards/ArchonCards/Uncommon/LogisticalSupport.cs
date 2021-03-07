using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Uncommon
{
    public class LogisticalSupport : AbstractCard
    {
        public LogisticalSupport()
        {
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            this.SetCommonCardAttributes(
                "Logistical Support",
                Rarity.UNCOMMON,
                TargetType.ALLY,
                CardType.SkillCard,
                1
                );
            this.BaseDamage = 5;
        }

        public override string DescriptionInner()
        {
            return $"The next time you play any card, play it again.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            
        }
        /// duplicate the next card played by anyone
    }
}