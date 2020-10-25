using UnityEngine;
using System.Collections;
using System;

public static class BattleRules
{

    public static int CalculateEnergyCost(AbstractCard card)
    {
        var owner = card.Owner;
        var ownerFatigue = owner.CurrentFatigue;
        if (ownerFatigue <= 0)
        {
            return card.BaseEnergyCost() + 1;
        }
        else
        {
            return card.BaseEnergyCost();
        }
    }

    public static void ProcessPlayingCardCost(AbstractCard card)
    {
        ServiceLocator.GetGameStateTracker().energy -= CalculateEnergyCost(card);
        EnergyIcon.Instance.Flash();
        if (card.Owner.CurrentFatigue > 1)
        {
            card.Owner.CurrentFatigue -= 1;
        }
    }

    public static void ProcessPreModifierDamage(AbstractBattleUnit source, AbstractBattleUnit target, int baseDamage)
    {
        var totalDamageAfterModifiers = GetAnticipatedDamageToUnit(source, target, baseDamage);
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
        foreach(var attribute in source.StatusEffects)
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
