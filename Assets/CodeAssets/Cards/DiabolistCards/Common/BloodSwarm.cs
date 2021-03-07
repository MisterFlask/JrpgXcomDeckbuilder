using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.GameLogic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Common
{
    public class BloodSwarm : AbstractCard
    {
        public BloodSwarm()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Bloodswarm", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 1);
            this.CardTags.Add(BattleCardTags.SWARM);
        }

        public override string DescriptionInner()
        {
            return "Deal 6 damage and gain 2 damage for the rest of the combat.  " +
                "Bloodprice.  " +
                "Swarm. " +
                "If bloodprice is paid, gains 1 damage PERMANENTLY.";
        }

        public override EnergyPaidInformation GetNetEnergyCost()
        {
            return BloodpriceBattleRules.GetNetEnergyCostWithBloodprice(this);
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            if (energyPaid.ActionsToTake.Any(item => item is BloodpricePaidAction))
            {
                this.CorrespondingPermanentCard().BaseDamage++;
                this.BaseDamage++;
            }
            action().AttackUnitForDamage(target, this.Owner, BaseDamage);
        }

        public override void InHandAtEndOfTurnAction()
        {
            SwarmBattleRules.RunSwarmBattleRules(this);
        }

        // Deal 6 damage and gain 2 damage for the rest of the combat.  Bloodprice.  Swarm. If bloodprice is paid, 
    }
}