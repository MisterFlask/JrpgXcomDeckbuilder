using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards.SifterCards.Rare
{
    public class Stonks : AbstractCard
    {
        //   Deal 10 damage.  Lethal: Increase this card's Hoard value by 2 PERMANENTLY.   Hoard 2.

        public Stonks()
        {
            SetCommonCardAttributes("Stonks!", Rarity.RARE, TargetType.ENEMY, CardType.AttackCard, 1);
            DamageModifiers.Add(new LethalTriggerDamageModifier("Lethal: Increase this card's Hoard value by 2 PERMANENTLY.", (unitKilled) =>
            {
                var gildedCard = this.CorrespondingPermanentCard().GetStickerOfType<GildedCardSticker>();
                if (gildedCard != null)
                {
                    gildedCard.GildedValue += 2;
                }
            }));
            Stickers.Add(new BasicAttackTargetSticker());
            Stickers.Add(new GildedCardSticker(2));
            BaseDamage = 10;
        }

        public override string DescriptionInner()
        {
            return "";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
        }
    }
}