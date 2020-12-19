using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Responsible for going through all the combat hooks and such that play into how much damage is dealt, by whom, what attributes are applied or removed, and so on.
/// </summary>
public static class BattleRules
{

    public static void ProcessHooksWhenStatusEffectAppliedToUnit(AbstractBattleUnit unitAffected, AbstractStatusEffect effectApplied, int stacksAppliedOrDecremented)
    {
        foreach(var status in unitAffected.StatusEffects)
        {
            var incOrDec = StatusEffectChange.DECREASE;

            if (stacksAppliedOrDecremented > 0) 
            {
                incOrDec = StatusEffectChange.INCREASE;
            }

            if (status.GetType() == effectApplied.GetType())
            {
                if (incOrDec == StatusEffectChange.INCREASE)
                {
                    status.OnApplicationOrIncrease();
                }
            }
            else
            {
                status.OnAnyStatusEffectApplicationToOwner(incOrDec, effectApplied);
            }
        }


    }

    public static int CalculateEnergyCost(AbstractCard card)
    {
        if (card.Owner == null)
        {
            return card.BaseEnergyCost();
        }

        var owner = card.Owner;
        var ownerFatigue = owner.CurrentFatigue;
        if (ownerFatigue < card.FatigueCost)
        {
            return card.EnergyCost + card.FatigueCost;
        }
        else
        {
            return card.EnergyCost;
        }
    }

    public static void ProcessPlayingCardCost(AbstractCard card)
    {
        ServiceLocator.GetGameStateTracker().energy -= CalculateEnergyCost(card);
        EnergyIcon.Instance.Flash();
        if (card.Owner.CurrentFatigue > card.FatigueCost)
        {
            card.Owner.CurrentFatigue -= card.FatigueCost;
        }
    }

    /// <summary>
    /// Source is allowed to be null in cases where isAttack is false. 
    /// </summary>
    public static void ProcessDamageWithCalculatedModifiers(AbstractBattleUnit damageSource, AbstractBattleUnit target, int baseDamage, bool isAttack = true)
    {
        int totalDamageAfterModifiers = baseDamage;
        if (isAttack)
        {
            totalDamageAfterModifiers = GetAnticipatedDamageToUnit(damageSource, target, baseDamage, isAttack);
        }


        var damageDealtAfterBlocking = totalDamageAfterModifiers;
        if (target.CurrentBlock >= totalDamageAfterModifiers)
        {
            damageDealtAfterBlocking = 0;
            target.CurrentBlock -= totalDamageAfterModifiers;
        }
        else
        {
            damageDealtAfterBlocking -= target.CurrentBlock;

            target.CurrentBlock = 0;
        }
        if (damageDealtAfterBlocking != 0)
        {
            var damageBlob = new DamageBlob
            {
                Damage = damageDealtAfterBlocking,
                IsAttackDamage = isAttack
            };

            if (target.CurrentHp > 0)
            {
                foreach(var effect in target.StatusEffects)
                {
                    effect.ModifyPostBlockDamageTaken(damageBlob);
                }
            }

            target.CurrentHp -= damageBlob.Damage;

            if (target.CurrentHp > 0 && isAttack)
            {
                ProcessAttackDamageReceivedHooks(damageSource, target, damageBlob.Damage);
            }

            ProcessHpLossHooks(damageSource, target, damageBlob.Damage);

            CheckAndRegisterDeath(target, damageSource);
        }
    }

    private static void ProcessHpLossHooks(AbstractBattleUnit damageSource, AbstractBattleUnit target, object damage)
    {
        //todo

    }

    public static void CheckAndRegisterDeath(AbstractBattleUnit unit, AbstractBattleUnit nullableUnitThatKilledMe)
    {

        if (unit.CurrentHp <= 0)
        {
            foreach (var effect in unit.StatusEffects)
            {
                effect.OnDeath(nullableUnitThatKilledMe);
            }
            ActionManager.Instance.DestroyUnit(unit);
        }
    }

    private static void ProcessAttackDamageReceivedHooks(AbstractBattleUnit source, AbstractBattleUnit target, int damageAfterBlockingAndModifiers)
    {
        foreach (var statusEffect in target.StatusEffects)
        {
            statusEffect.OnStruck(source, damageAfterBlockingAndModifiers);
        }
        foreach (var statusEffect in source.StatusEffects)
        {
            statusEffect.OnStriking(source, damageAfterBlockingAndModifiers);
        }

        ProcessSpecialDamagedRules(source, target, damageAfterBlockingAndModifiers);
    }

    /// <summary>
    /// This deals with stress, and any other damage triggers not covered by status effects.
    /// </summary>

    private static void ProcessSpecialDamagedRules(AbstractBattleUnit source, AbstractBattleUnit target, int damageAfterBlockingAndModifiers)
    {
        if (damageAfterBlockingAndModifiers > 0)
        {
            // characters take 2 stress after taking damage.
            ActionManager.Instance.ApplyStatusEffect(target, new StressStatusEffect(), 2);
        }
    }

    public static int GetDefenseApplied(AbstractBattleUnit source, AbstractBattleUnit target, int baseDefense)
    {

        // first, go through the attacker's attributes
        float currentTotalDefense = baseDefense;
        foreach (var attribute in source.StatusEffects)
        {
            currentTotalDefense *= attribute.DefenseDealtMultiplier();
            currentTotalDefense += attribute.DefenseDealtAddition();
        }

        // then, go through defender's attributes

        foreach (var attribute in source.StatusEffects)
        {
            currentTotalDefense *= attribute.DefenseReceivedMultiplier();
            currentTotalDefense += attribute.DefenseReceivedAddition();
        }

        return (int)currentTotalDefense;
    }

    public static int GetAnticipatedDamageToUnit(AbstractBattleUnit source, AbstractBattleUnit target, int baseDamage, bool isAttackDamage)
    {
        // first, go through the attacker's attributes
        float currentTotalDamage = baseDamage;
        foreach (var attribute in source.StatusEffects)
        {
            currentTotalDamage *= attribute.DamageDealtMultiplier();
            currentTotalDamage += attribute.DamageDealtAddition();
        }

        // then, go through defender's attributes

        foreach (var attribute in target.StatusEffects)
        {
            currentTotalDamage *= attribute.DamageReceivedMultiplier();
            currentTotalDamage += attribute.DamageReceivedAddition();
        }

        if (currentTotalDamage < 0)
        {
            return 0;
        }


        return (int) currentTotalDamage;
    }

    internal static bool CanFallBack(AbstractBattleUnit underlyingEntity)
    {
        return (underlyingEntity.IsAdvanced);
    }

    internal static bool CanAdvance(AbstractBattleUnit underlyingEntity)
    {
        return (!underlyingEntity.IsAdvanced);
    }

    public static string GetAdvanceOrFallBackButtonText(AbstractBattleUnit underlyingEntity)
    {
        if (CanFallBack(underlyingEntity))
        {
            return "Fall Back";
        }
        else if (CanAdvance(underlyingEntity))
        {
            return "Advance";
        }

        return "Can't Move";
    }

    public static int GetDisplayedDamageOnCard(AbstractCard card)
    {
        if (card.Owner == null)
        {
            // This branch is for card rewards.
            return card.BaseDamage;
        }

        float baseDamage = card.BaseDamage;

        foreach(var attribute in card.Owner.StatusEffects)
        {
            baseDamage *= attribute.DamageDealtMultiplier();
            baseDamage += attribute.DamageDealtAddition();
        }

        return (int)baseDamage;
    }
    public static int GetDisplayedDefenseOnCard(AbstractCard card)
    {
        if (card.Owner == null)
        {
            // This branch is for card rewards.
            return card.BaseDefenseValue;
        }

        float baseDamage = card.BaseDefenseValue;
        foreach (var attribute in card.Owner.StatusEffects)
        {
            baseDamage *= attribute.DefenseDealtMultiplier();
            baseDamage += attribute.DefenseDealtAddition();
        }

        return (int)baseDamage;
    }

    public static void CheckIsBattleOver()
    {
        var isVictory = GameState.Instance.EnemyUnitsInBattle.IsEmpty();
        var isDefeat = GameState.Instance.CurrentMission.IsFailed();

        if (isVictory)
        {
            GameState.Instance.CurrentMission.OnSuccess();
        }
        if (isDefeat)
        {
            GameState.Instance.CurrentMission.IsFailure = true;
            GameState.Instance.CurrentMission.OnFailed();
        }
        var isOver = (isVictory || isDefeat) ;
        if (isOver)
        {
            GameScenes.SwitchToBattleResultScene();
        }
    }


    public static void ProcessRetreat()
    {
        foreach(var character in GameState.Instance.AllyUnitsInBattle)
        {
            ActionManager.Instance.ApplyStatusEffect(character, new RetreatingStatusEffect(), 2);
        }
    }
}


public class RetreatingStatusEffect : AbstractStatusEffect
{
    public RetreatingStatusEffect()
    {
        Name = "Retreating";
    }

    public override string Description => "Stacks decreaese by 1 every turn.  When it reaches zero stacks, flee combat.";

    public override void OnTurnStart()
    {
        Stacks--;

        if (Stacks == 0)
        {
            ActionManager.Instance.FleeCombat();
        }
    }



}