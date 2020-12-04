using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Responsible for going through all the combat hooks and such that play into how much damage is dealt, by whom, what attributes are applied or removed, and so on.
/// </summary>
public static class BattleRules
{

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
    public static void ProcessPreModifierDamage(AbstractBattleUnit source, AbstractBattleUnit target, int baseDamage, bool isAttack = true)
    {
        int totalDamageAfterModifiers = baseDamage;
        if (isAttack)
        {
            totalDamageAfterModifiers = GetAnticipatedDamageToUnit(source, target, baseDamage);
        }
        var damageDealtToHp = totalDamageAfterModifiers;
        if (target.CurrentDefense >= totalDamageAfterModifiers)
        {
            damageDealtToHp = 0;
            target.CurrentDefense -= totalDamageAfterModifiers;
        }
        else
        {
            damageDealtToHp -= target.CurrentDefense;

            target.CurrentHp -= damageDealtToHp;
            target.CurrentDefense = 0;
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

    public static int GetAnticipatedDamageToUnit(AbstractBattleUnit source, AbstractBattleUnit target, int baseDamage)
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
}
