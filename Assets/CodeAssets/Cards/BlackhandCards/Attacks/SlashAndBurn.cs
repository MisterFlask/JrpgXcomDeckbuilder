using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Attacks
{
    public class SlashAndBurn : AbstractCard
    {
        public SlashAndBurn()
        {
            SetCommonCardAttributes("Slash And Burn", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 0);
        }

        public override string Description()
        {
            return $"Deal 10 damage.  Manuever.";
        }

        protected override void OnPlay(AbstractBattleUnit target)
        {
            ActionManager.Instance.ApplyStatusEffect(target, new BurningStatusEffect(), 3);
            this.EnergyCostMod += 1;
        }
    }
}