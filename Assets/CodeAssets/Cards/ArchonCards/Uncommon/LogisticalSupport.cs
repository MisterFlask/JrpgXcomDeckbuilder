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
            return $"The next time you play ANY card, play it again.  Planned.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            
        }
        /// duplicate the next card played by anyone
    }


    public class LogisticalSupportStatusEffect: AbstractStatusEffect
    {
        public LogisticalSupportStatusEffect()
        {
            Name = "Logistical Support";
        }

        public override string Description => "Duplicates the next card played by ANY character.";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool isMine)
        {
            var target = targetOfCard;
            if (target.IsDead)
            {
                target = CardTargeting.RandomTargetableEnemy();
            }
            cardPlayed.EvokeCardEffect(target, new EnergyPaidInformation());
        }
    }
}