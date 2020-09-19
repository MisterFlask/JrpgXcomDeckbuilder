using UnityEngine;
using System.Collections;

public static class BattleRules
{
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
        foreach (var attribute in source.Attributes)
        {
            currentTotalDefense *= attribute.DefenseDealtMultiplier();
            currentTotalDefense += attribute.DefenseDealtAddition();
        }

        // then, go through defender's attributes

        foreach (var attribute in source.Attributes)
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
        foreach(var attribute in source.Attributes)
        {
            currentTotalDamage *= attribute.DamageDealtMultiplier();
            currentTotalDamage += attribute.DamageDealtAddition();
        }

        // then, go through defender's attributes

        foreach (var attribute in source.Attributes)
        {
            currentTotalDamage *= attribute.DamageReceivedMultiplier();
            currentTotalDamage += attribute.DamageReceivedAddition();
        }

        return (int) currentTotalDamage;
    }

    public static int GetDisplayedDamageOnCard(AbstractCard card)
    {
        float baseDamage = card.BaseDamage;
        foreach(var attribute in card.Owner.Attributes)
        {
            baseDamage *= attribute.DamageDealtMultiplier();
            baseDamage += attribute.DamageDealtAddition();
        }

        return (int)baseDamage;
    }
}
