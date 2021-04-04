using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards.SifterCards.Rare
{
    public class FinancialAdvisor : AbstractCard
    {
        // deal 20 damage.  Slay: A random card in an ally's deck gains Hoard 2 PERMANENTLY.  Cost 2.

        public FinancialAdvisor()
        {
            SetCommonCardAttributes("Financial Advisor", Rarity.RARE, TargetType.ENEMY, CardType.AttackCard, 2);
            DamageModifiers.Add(new LethalTriggerDamageModifier("A random card in an ally's deck gains Hoard 2 PERMANENTLY.", (deadUnit) =>
            {
                var alliesNotMe = state().AllyUnitsInBattle.Where(item => item != this.Owner && !item.IsDead);
                var randomCard = alliesNotMe.SelectMany(item => item.CardsInPersistentDeck).Shuffle().FirstOrDefault();
                if (randomCard != null)
                {
                    randomCard.Stickers.Add(new GildedCardSticker(2));
                }
            }));
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} to target.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
        }
    }
}