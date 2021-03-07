using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
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
            return $"Manuever.  Leadership: the target gains 1 strength and 1 dexterity.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            if (target.HasStatusEffect<AdvancedStatusEffect>())
            {
                action().RemoveStatusEffect<AdvancedStatusEffect>(target);
            }
            else
            {
                action().ApplyStatusEffect(target, new AdvancedStatusEffect(), 1);
            }
        }
    }
}