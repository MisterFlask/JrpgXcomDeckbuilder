using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards.Common
{
    public class OverThreeHundredConfirmedKills : AbstractCard
    {
        // deal 5 damage and Owner gains 2 block.  Cost 1.
        // Lethal: ALL copies of this card owned by {Owner} gains 1 defense and 1 attack PERMANENTLY.
        // Brute:  Draw 3 cards.
        

        public OverThreeHundredConfirmedKills()
        {
            SetCommonCardAttributes("Over Three Hundred Confirmed Kills", Rarity.COMMON, TargetType.ENEMY, CardType.AttackCard, 1, typeof(HammerSoldierClass));
            BaseDamage = 5;
            BaseDefenseValue = 2;
            DamageModifiers.Add(new LethalTriggerDamageModifier("ALL copies of this card owned by this character gain 1 defense and 1 attack PERMANENTLY.", (character) =>
            {
                // todo: get all persistent copies of card, increase damage etc etc
            }));
        }

        public override string DescriptionInner()
        {
            return $"deal {DisplayedDamage()} damage and Owner gains {DisplayedDefense()} block.  Lethal: ALL copies of this card owned by {ownerDisplayString()} gains 1 defense and 1 attack PERMANENTLY.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            Action_ApplyDefenseToTarget(Owner);
        }
    }
}