using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards
{
    public class ArmorSaves : AbstractCard
    {
        // Apply 5 Barricade. [damage resist, halves each turn]  Exhaust.  Draw a card.

        public ArmorSaves()
        {
            this.SetCommonCardAttributes("Armor Save", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 1);
        }

        public override string DescriptionInner()
        {
            return $"Apply 5 Barricade.  Draw a card.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToTarget(new BarricadeStatusEffect(), 5, target);
            action().DrawCards(1);
            this.ExhaustAsAction();
        }
    }
}