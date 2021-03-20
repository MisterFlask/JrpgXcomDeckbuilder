using System;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Augmentations
{
    public static class BasicAugmentations
    {


        public static SoldierPerk GrantsPowerAugmentation = new GrantsStatusEffectPerk("Power Fist", "Increases this soldier's Power by 1.", new StrengthStatusEffect(), 1);
        public static SoldierPerk GrantsAblativeArmorAugmentation = new GrantsStatusEffectPerk("Ablative Armor", "Decreases ALL damage taken by 1", new ArmoredStatusEffect(), 1);
        public static SoldierPerk IncreaseHpOnGettingNewCardsPerk = new HpGainOnCardAddedPerk();
    }

    public class DealGreaterDamageToEnemiesWithStatusEffectPerk : SoldierPerk
    {
        int DamageChange { get; set; }
        AbstractStatusEffect TargetEffect { get; set; }
        string MyName { get; set; }
        public DealGreaterDamageToEnemiesWithStatusEffectPerk(
            AbstractStatusEffect abstractStatusEffectType,
            int damageChange,
            string name)
        {
            DamageChange = damageChange;
            TargetEffect = abstractStatusEffectType;
            this.MyName = name;
        }

        public override string Description()
        {
            return $"Deals {Stacks} greater damage to enemies afflicted with " + TargetEffect.Name;
        }

        public override string Name()
        {
            return MyName;
        }

        public override void PerformAtBeginningOfCombat(AbstractBattleUnit soldierAffected)
        {
            soldierAffected.ApplyStatusEffect(new DealsExtraDamageToBurningStatusEffect(), Stacks);
        }
    }

    public class DealsExtraDamageToBurningStatusEffect: AbstractDamageModifierToEnemiesWithStatusEffect
    {
        public DealsExtraDamageToBurningStatusEffect() : base(new BurningStatusEffect())
        {
        }
    }

    /// <summary>
    /// Note: made this an abstract class because I'm just having it be an invariant that if two status effects have the same class,
    /// they ARE the same status effect.
    /// </summary>
    public abstract class AbstractDamageModifierToEnemiesWithStatusEffect : AbstractStatusEffect
    {
        public AbstractStatusEffect TargetStatusEffect { get; private set; }
        public AbstractDamageModifierToEnemiesWithStatusEffect(AbstractStatusEffect targetStatusEffect)
        {
            Name = "Damage Mod: " + targetStatusEffect.Name;
            TargetStatusEffect = targetStatusEffect;
        }

        public override string Description =>  $"Deals {Stacks} greater damage to enemies afflicted with " + TargetStatusEffect?.Name ?? "UNKNOWN";

    }

    public class DuplicateNextCardTwicePerk : SoldierPerk
    {
        public DuplicateNextCardTwicePerk()
        {

        }

        public override void ModifyCardsUponAcquisition(AbstractCard card, AbstractBattleUnit soldierAffected)
        {
            if (Stacks <= 0)
            {
                return;
            }
            Stacks--;
            soldierAffected.AddCardToPersistentDeck(card.CopyCard());
            soldierAffected.AddCardToPersistentDeck(card.CopyCard());
        }

        public override string Name()
        {
            return "Unsupervised Learning";
        }

        public override string Description()
        {
            return "Whenever you add a card to your deck, add a duplicate of that card TWICE.";
        }
    }

    public class HpGainOnCardAddedPerk : SoldierPerk
    {
        public HpGainOnCardAddedPerk()
        {

        }

        public override void ModifyCardsUponAcquisition(AbstractCard card, AbstractBattleUnit soldierAffected)
        {
            soldierAffected.MaxHp += Stacks;
        }

        public override string Name()
        {
            return "Supervised Learner Module";
        }

        public override string Description()
        {
            return $"Whenever you add a card to your deck, increase your HP by {Stacks}.";
        }
    }

}