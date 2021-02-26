using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Common
{
    public class MountingAdvantages : AbstractCard
    {
        public MountingAdvantages()
        {
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            this.SetCommonCardAttributes(
                "Mounting Advantages",
                Rarity.UNCOMMON,
                TargetType.ALLY,
                CardType.PowerCard,
                2
                );
            this.BaseDamage = 5;
        }

        public override string Description()
        {
            return $"ALL allies gain 1 Dragoon's Mark";
        }

        protected override void OnPlay(AbstractBattleUnit target)
        {
            action().ApplyStatusEffect(target, new AdvancedStatusEffect(), 1);
        }
    }
}