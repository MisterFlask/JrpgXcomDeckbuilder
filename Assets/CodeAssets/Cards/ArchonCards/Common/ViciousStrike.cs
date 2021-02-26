using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Common
{
    public class ViciousStrike : AbstractCard
    {
        public ViciousStrike()
        {
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            this.SetCommonCardAttributes(
                "Fanatical Charge",
                Rarity.COMMON,
                TargetType.ENEMY,
                CardType.AttackCard,
                1
                );
            this.BaseDamage = 5;
        }

        public override string Description()
        {
            return $"Deal {DisplayedDamage()} damage to a random enemy.  If I have >40 stress, do it again.  If I have >70 stress, do it again.";
        }

        protected override void OnPlay(AbstractBattleUnit target)
        {
            action().AttackUnitForDamage(target, Owner, BaseDamage);
            if (Owner.CurrentStress > 40)
            {
                action().AttackUnitForDamage(target, Owner, BaseDamage);
            }
            if (Owner.CurrentStress > 70)
            {
                action().AttackUnitForDamage(target, Owner, BaseDamage);
            }
        }
    }
}