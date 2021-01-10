using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Collections.Generic;

public class CardRegistrar 
{ 

    private static Dictionary<Type, List<AbstractCard>> ReflectiveCardCache = new Dictionary<Type, List<AbstractCard>>();

    public static void InitCardsReflectively()
    {
        ReflectiveCardCache.Clear();
        var types = Assembly
          .GetExecutingAssembly()
          .GetTypes();

        foreach (var t in types)
        {
            var registeredAttributes = t.GetCustomAttributes<RegisteredCardAttribute>();
            foreach(var registeredAttribute in registeredAttributes)
            {
                var type = registeredAttribute.SoldierClass;
                if (!ReflectiveCardCache.ContainsKey(t))
                {
                    ReflectiveCardCache[t] = new List<AbstractCard>();
                }
                var card = Activator.CreateInstance(t) as AbstractCard;
                if (card == null)
                {
                    throw new Exception("Could not get registered card!: " + t.Name);
                }
                ReflectiveCardCache[t].Add(card);
            }
        }
    }

    public List<AbstractCard> GetCardPool(Type soldierClass)
    {
        return ReflectiveCardCache[soldierClass];
    }
}


public class RegisteredCardAttribute: Attribute
{
    public Type SoldierClass;
}
