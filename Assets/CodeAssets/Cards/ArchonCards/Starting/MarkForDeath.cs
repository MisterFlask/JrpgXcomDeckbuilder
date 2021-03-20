using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Starting
{
    public class MarkForDeath : AbstractCard
    {
        public MarkForDeath()
        {

            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass)); //todo: remove, this is a starting card
            this.SetCommonCardAttributes(
                "Mark For Death",
                Rarity.COMMON,
                TargetType.ENEMY,
                CardType.AttackCard,
                1
                );

            BaseDamage = 5;
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()}.  Apply Marked.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new MarkedStatusEffect());
            action().AttackUnitForDamage(target, Owner, BaseDamage, this);
        }
    }
}