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

        public override string Description()
        {
            return $"If target is Advanced, remove Advanced.  Otherwise, apply Advanced.";
        }

        protected override void OnPlay(AbstractBattleUnit target)
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