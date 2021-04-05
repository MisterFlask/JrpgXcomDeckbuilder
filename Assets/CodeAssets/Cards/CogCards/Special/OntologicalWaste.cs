using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Special
{
    public class OntologicalWaste : AbstractCard
    {
        // Playable for 1.  Retained: A random character takes 3 Stress.
        public OntologicalWaste()
        {
            SetCommonCardAttributes("Ontological Waste", Rarity.NOT_IN_POOL, TargetType.NO_TARGET_OR_SELF, CardType.StatusCard, 1);
            this.NonmodifiableStickers.Add(new ExhaustCardSticker());
        }

        public override string DescriptionInner()
        {
            return $"Retained: A random character suffers 3 stress.";

        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            
        }

        public override void InHandAtEndOfTurnAction()
        {
            action().ApplyStress(state().AllyUnitsInBattle.PickRandomWhere(item => item.IsTargetable()), 3);
        }
    }
}