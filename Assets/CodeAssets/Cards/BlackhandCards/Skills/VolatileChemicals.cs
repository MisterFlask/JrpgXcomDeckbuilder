using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.Attacks
{
    public class VolatileChemicals : AbstractCard
    {
        public VolatileChemicals()
        {
            SetCommonCardAttributes("Volatile Chemicals", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 0);
        }

        public override string DescriptionInner()
        {
            return "Sacrifice: Gain 2 Energy.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            this.Sacrifice(() =>
            {
                action().DoAThing(() =>
                {
                    state().energy+=2;
                });
            });
        }
    }
}