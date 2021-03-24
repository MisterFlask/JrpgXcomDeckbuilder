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

        public override string DescriptionInner()
        {
            return $"Deal 10 damage.  Inferno: Draw a card.  Ambush: Then deal another 5 damage.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().AttackWithCard(this, target);
            CardAbilityProcs.Inferno(this, () =>
            {
                action().DrawCards(1);
            });
        }
    }
}