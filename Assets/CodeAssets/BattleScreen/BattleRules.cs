using UnityEngine;
using System.Collections;

public static class BattleRules
{

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
