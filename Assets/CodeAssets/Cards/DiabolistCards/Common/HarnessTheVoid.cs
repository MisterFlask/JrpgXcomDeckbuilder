using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Common
{
    public class HarnessTheVoid : AbstractCard
    {
        // Gain 10 Temp HP
        // Sacrifice: ALL allies gain 1 strength.

        public HarnessTheVoid()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Harness the Void", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 2);
        }


        public override string DescriptionInner()
        {
            return "Apply 10 temp HP.  Sacrifice: exhaust, and ALL allies gain 1 strength.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new TemporaryHpStatusEffect(), 10);

            this.Sacrifice(() =>
            {
                this.ExhaustAsAction();
                foreach (var ally in state().AllyUnitsInBattle)
                {
                    action().ApplyStatusEffect(target, new StrengthStatusEffect(), 1);
                }
            });
        }

    }
}