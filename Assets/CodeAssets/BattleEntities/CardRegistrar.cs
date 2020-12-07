using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Collections.Generic;

public class CardRegistrar : MonoBehaviour
{
    private Dictionary<Type, List<AbstractCard>> cardCache = new Dictionary<Type, List<AbstractCard>>();

    public void Init()
    {
        cardCache.Clear();
        var types = Assembly
          .GetExecutingAssembly()
          .GetTypes();

        foreach (var t in types)
        {
            var registeredAttributes = t.GetCustomAttributes<RegisteredCardAttribute>();
            foreach(var registeredAttribute in registeredAttributes)
            {
                var type = registeredAttribute.SoldierClass;
                if (!cardCache.ContainsKey(t))
                {
                    cardCache[t] = new List<AbstractCard>();
                }
                var card = Activator.CreateInstance(t) as AbstractCard;
                if (card == null)
                {
                    throw new Exception("Could not get registered card!: " + t.Name);
                }
                cardCache[t].Add(card);
            }
        }
    }

    public List<AbstractCard> GetCardPool(Type soldierClass)
    {
        return cardCache[soldierClass];
    }
}

public class RegisteredCardAttribute: Attribute
{
    public Type SoldierClass;
}
