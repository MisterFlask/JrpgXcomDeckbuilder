using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Common
{
    public class EyeForAnEye : AbstractCard
    {
        private int StacksOfRetaliateToApply = 2;

        public EyeForAnEye()
        {
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            this.SetCommonCardAttributes(
                "Eye for an Eye",
                Rarity.COMMON,
                TargetType.ALLY,
                CardType.PowerCard,
                0
                );


        }

        public override string Description()
        {
            return $"Apply {StacksOfRetaliateToApply} Thorns.  If the character has >40 stress, do it again.";
        }

        protected override void OnPlay(AbstractBattleUnit target)
        {
            action().ApplyStatusEffect(target, new ThornsStatusEffect(), StacksOfRetaliateToApply);
        }
    }
}