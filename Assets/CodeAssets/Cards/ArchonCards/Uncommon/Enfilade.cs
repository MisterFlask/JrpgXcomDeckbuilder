using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Uncommon
{
    public class Enfilade : AbstractCard
    {
        public Enfilade()
        {
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            this.SetCommonCardAttributes(
                "Enfilade",
                Rarity.UNCOMMON,
                TargetType.ALLY,
                CardType.SkillCard,
                0
                );
            this.BaseDamage = 5;
        }

        public override string DescriptionInner()
        {
            return $"Manuever the target.  Leadership: the target gains 1 strength and 1 dexterity.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            ArchonBattleRules.Manuever(target);
        }
    }
}