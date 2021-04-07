using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.SifterCards.Uncommon
{
    public class PawnBroker : AbstractCard
    {
        // Gain 5 block.  Whenever you trigger Sacrifice, ALL characters gain 1 Empowered.  Cost 1.
        public override string DescriptionInner()
        {
            return "Gain 5 block.  Whenever you trigger sacrifice, ALL allies gain 1 Empowered.  Exhaust.";

        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyDefenseToTarget(Owner, BaseDefenseValue);
            Action_ApplyStatusEffectToOwner(new PawnBrokerStatusEffect(), 1);
            Action_Exhaust();
        }
    }

    public class PawnBrokerStatusEffect : AbstractStatusEffect
    {
        public PawnBrokerStatusEffect()
        {
            Name = "Pawn Broker";
        }

        public override string Description => $"Whenever you trigger Sacrifice, ALL allies gain {DisplayedStacks()} Empowered.";


        public override void ProcessProc(AbstractProc proc)
        {
            if (proc is SacrificeProc)
            {
                foreach(var ally in state().AllyUnitsInBattle)
                {
                    action().ApplyStatusEffect(ally, new EmpoweredStatusEffect(), Stacks);
                }
            }
        }
    }

}