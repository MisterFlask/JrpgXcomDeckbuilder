using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Common
{
    public class FanaticalCharge : AbstractCard
    {
        public FanaticalCharge()
        {
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            this.SetCommonCardAttributes(
                "Fanatical Charge",
                Rarity.COMMON,
                TargetType.ALLY,
                CardType.AttackCard,
                1
                );
            this.BaseDamage = 5;
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage to a random enemy.  Advance target character.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().AttackUnitForDamage(CardTargeting.RandomTargetableEnemy(), Owner, BaseDamage);
            action().ApplyStatusEffect(target, new AdvancedStatusEffect(), 1);
        }
    }
}