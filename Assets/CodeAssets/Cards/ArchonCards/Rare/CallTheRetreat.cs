using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Starting
{
    public class CallTheRetreat : AbstractCard
    {
        public CallTheRetreat()
        {

            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass)); // todo: remove
            this.SetCommonCardAttributes(
                "F*** This, We Out",
                Rarity.RARE,
                TargetType.NO_TARGET_OR_SELF,
                CardType.SkillCard,
                1
                );

            this.BaseDefenseValue = 5;
        }

        public override string Description()
        {
            return $"Flee the combat instantly.  ALL characters gain 10 stress.";
        }

        protected override void OnPlay(AbstractBattleUnit target)
        {
            // todo
            // 
        }
    }
}