using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Starting
{
    public class GiveGround : AbstractCard
    {
        public GiveGround()
        {

            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass)); // todo: remove
            this.SetCommonCardAttributes(
                "Give Ground!",
                Rarity.COMMON,
                TargetType.ALLY,
                CardType.SkillCard,
                1
                );

            this.BaseDefenseValue = 5;
        }

        public override string Description()
        {
            return $"Remove Advanced from an ally.  Apply {DisplayedDefense()} defense.";
        }

        protected override void OnPlay(AbstractBattleUnit target)
        {
            action().RemoveStatusEffect<AdvancedStatusEffect>(target);
            action().ApplyDefense(target, Owner, BaseDefenseValue);
        }
    }
}