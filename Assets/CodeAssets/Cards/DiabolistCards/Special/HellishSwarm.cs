using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Special
{
    public class HellishSwarm : AbstractCard
    {
        public HellishSwarm()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Hellish Swarm", Rarity.NOT_IN_POOL, TargetType.ENEMY, CardType.AttackCard, 0);
            this.CardTags.Add(BattleCardTags.SWARM);
            this.BaseDamage = 4;
        }

        // Deal 4 damage and apply 1 Vulnerable.  Draw a card.  Exhaust.  Cost 0.
        // Swarm.
        public override string DescriptionInner()
        {
            return "Deal 4 damage and apply 1 Vulnerable.  Draw a card.  Exhaust.  Cost 0.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new VulnerableStatusEffect(), 1);
            action().DrawCards(1);
            this.Action_Exhaust();
        }
    }
}