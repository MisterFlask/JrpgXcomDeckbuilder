using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards.Rare
{
    public class Bastion : AbstractCard
    {
        // apply 10 Barricade to ALL allies. [10 damage resist, halves each turn.  Barricade scales with Dexterity.]  Cost 3, Refund 1.  Exhaust.

        public Bastion()
        {
            SetCommonCardAttributes("Bastion", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 3);
        }

        public override string DescriptionInner()
        {
            return $"apply 10 Barricade to ALL allies.  Refund 1.  Exhaust";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach(var ally in state().AllyUnitsInBattle)
            {
                Action_ApplyStatusEffectToTarget(new BarricadeStatusEffect(), 10, ally);
            }
            CardAbilityProcs.Refund(this);
            this.Action_Exhaust();
        }
    }
}