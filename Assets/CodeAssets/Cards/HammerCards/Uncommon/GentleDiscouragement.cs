using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards.Uncommon
{
    public class GentleDiscouragement : AbstractCard
    {
        public GentleDiscouragement()
        {
            SetCommonCardAttributes("Gentle Discouragement", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1);
        }

        //  Retaliations do 7 more damage.  Gain 1 Retaliate.  Exhaust.
        public override string DescriptionInner()
        {
            return $"Retaliations do 7 more damage.  Gain 1 Retaliate.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(this.Owner, new GentleDiscouragementStatusEffect(), 7);
            action().ApplyStatusEffect(this.Owner, new RetaliateStatusEffect());
            this.Action_Exhaust();
        }
    }
}