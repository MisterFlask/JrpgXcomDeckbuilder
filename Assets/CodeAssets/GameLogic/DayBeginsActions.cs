using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DayBeginsActions
{
    public static void ApplyTriggers()
    {
        foreach(var character in GameState.Instance.PersistentCharacterRoster)
        {
            foreach(var perk in character.Perks)
            {
                perk.PerformAtBeginningOfNewDay(character);
            }
        }
    }
}

