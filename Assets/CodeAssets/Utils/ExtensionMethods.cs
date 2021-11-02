
using HyperCard;
using PathologicalGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public static class ExtensionMethods
{

    public static void ForEachCreateAction<T>(this IEnumerable<T> items, Action<T> toPerform)
    {
        foreach(var item in items)
        {
            ActionManager.Instance.DoAThing(() => toPerform(item));
        }
    }

    public static bool IsExhausted(this AbstractCard card)
    {
        return GameState.Instance.Deck.ExhaustPile.Contains(card);
    }
    public static bool IsInDrawPile(this AbstractCard card)
    {
        return GameState.Instance.Deck.DrawPile.Contains(card);
    }
    public static bool IsInDiscardPile(this AbstractCard card)
    {
        return GameState.Instance.Deck.DiscardPile.Contains(card);
    }

    public static IEnumerable<T> TakeUpTo<T>(this IEnumerable<T> list, int num)
    {
        var count = list.Count();
        return list.Take(count);
    }

    public static void InsertIntoBeginning<T>(this List<T> list, T item)
    {
        list.Insert(0, item);
    }

    public static void InsertIntoRandomLocation<T>(this List<T> list, T item)
    {
        var randomIndex = UnityEngine.Random.Range(0, list.Count);
        list.Insert(randomIndex, item);
    }

    public static List<AbstractBattleUnit> ConvertGuidsToSoldiers(this IEnumerable<string> guids)
    {
        return guids
         .Select(item => GameState.Instance.PersistentCharacterRoster
         .Single(soldierInRoster => soldierInRoster.Guid == item))
         .ToList();
    }
    public static List<AbstractCard> ConvertToCards(this IEnumerable<string> guids)
    {
        return guids
         .Select(item => GameState.Instance.PersistentCharacterRoster.SelectMany(character => character.CardsInPersistentDeck)
         .Single(card => card.Id == item))
         .ToList();
    }

    public static void AddTransientComponentAndPerformOperation<T>(this MonoBehaviour thisObject, Action<T> thingToDo, bool skipIfComponentExists = true) where T: MonoBehaviour
    {
        if (thisObject.gameObject.GetComponent<T>() != null && skipIfComponentExists)
        {
            return;
        }
        thisObject.gameObject.AddComponent<T>();
        thingToDo(thisObject.gameObject.GetComponent<T>());
    }
    public static Color WithAlpha(this Color color, float alpha)
    {
        var ret = new Color(color.r, color.g, color.b, alpha);
        return ret;
    }


    #region collections
    public static bool In<T>(this T item, IEnumerable<T> coll)
    {
        return coll.Contains(item);
    }
    public static bool NotIn<T>(this T item, IEnumerable<T> coll)
    {
        return !coll.Contains(item);
    }

    public static T PickRandom<T>(this IEnumerable<T> source)
    {
        return source.PickRandom(1).SingleOrDefault();
    }
    public static T PickRandomWhere<T>(this IEnumerable<T> source, Predicate<T> required)
    {
        return source.Where(item => required(item)).PickRandom(1).SingleOrDefault();
    }

    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
    {
        if (count > source.Count())
        {
            count = source.Count();
        }

        return source.Shuffle().Take(count);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }

    public static T PopFirstElement<T>(this IList<T> items)
    {
        if (items.Count == 0)
        {
            throw new Exception("No items left in this list");
        }
        var last = items.First();
        items.RemoveAt(0);
        return last;
    }

    public static bool IsEmpty<T>(this IEnumerable<T> items)
    {
        if (items == null) return true;
        if (items.Count() == 0)
        {
            return true;
        }
        return false;
    }

    public static List<T> Multiply<T>(this IEnumerable<T> items, int multiplyBy)
    {
        var newList = new List<T>();
        for(int i = 0; i < multiplyBy; i++)
        {
            newList.AddRange(items);
        }
        return newList;
    }

    public static List<T> TakeUnique<T>(this List<T> items, int numItems)
    {
        var newList = new List<T>();
        int itemsLeft = numItems;
        for (int i = 0; i < items.Count(); i++)
        {
            if (itemsLeft == 0)
            {
                return newList;
            }
            if (!newList.Contains(items[i]))
            {
                newList.Add(items[i]);
                itemsLeft--;
            }
        }
        return newList;
    }

    #endregion

    public static List<T> ToComponents<T>(this List<GameObject> objects)
    {
        return objects.Select(item => item.GetComponent<T>()).Where(item => item != null).ToList();
    }
    public static void SetZ(this GameObject obj, int z)
    {
        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, z);
    }

    public static List<Transform> GetChildren(this GameObject obj)
    {
        var l = new List<Transform>();
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            l.Add(obj.transform.GetChild(i));
        }
        return l;
    }

    public static void ReorderChildren(this GameObject obj, Func<Transform, float> orderingFunction)
    {
        var children = GetChildren(obj);
        var childrenInOrder = children.OrderBy(item => orderingFunction.Invoke(item));
        foreach(var child in childrenInOrder)
        {
            child.SetAsFirstSibling();
        }
    }
    public static void SortChildrenBasedOnSortableUiElements(this GameObject obj)
    {
        obj.ReorderChildren((transform) => {
            float value = 100000f;
            var orderableElement = transform.gameObject.GetComponent<OrderableUiElement>();
            if (orderableElement != null)
            {
                value = orderableElement.Order;
            }
            return value;
        });
    }

    public static List<Transform> GetChildren(this MonoBehaviour obj)
    {
        return GetChildren(obj.gameObject);
    }

    public static void PurgeChildrenIf<T>(this MonoBehaviour obj, Func<T, bool> func)
    {
        PurgeChildrenIf<T>(obj.gameObject, func);
    }

    public static void PurgeChildrenIf<T>(this GameObject obj, Func<T, bool> func)
    {
        foreach (var child in obj.GetChildren())
        {
            if (child.GetComponent<T>() == null)
            {
                continue;
            }
            if (func(child.GetComponent<T>()))
            {
                UnityEngine.Object.Destroy(child.gameObject);
            }
        }
    }

    public static void PurgeChildren(this MonoBehaviour obj)
    {
        for (int i = 0; i < obj.gameObject.transform.childCount; i++)
        {
            UnityEngine.Object.Destroy(obj.transform.GetChild(i).gameObject);
        }
    }
    public static void PurgeChildren(this GameObject obj)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            UnityEngine.Object.Destroy(obj.transform.GetChild(i).gameObject);
        }
    }

    public static void AddChild(this GameObject obj, GameObject childElement)
    {
        childElement.transform.SetParent(obj.transform);
    }

    public static void AddChildren(this GameObject obj, IEnumerable<GameObject> childElements)
    {
        foreach (var child in childElements)
        {
            child.transform.SetParent(obj.transform);
        }
    }

    public static void AddChildren(this MonoBehaviour obj, IEnumerable<GameObject> childElements)
    {
        foreach (var child in childElements)
        {
            child.transform.SetParent(obj.transform);
        }
    }


    private static SpawnPool spawnPool = null;

    private static SpawnPool GetSpawnPool()
    {
        if (spawnPool == null)
        {
            spawnPool = ServiceLocator.GetSpawnPool();
        }
        return spawnPool;
    }

    public static bool ContainsAll<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        foreach (var item in second)
        {
            if (!first.Contains(item))
            {
                return false;
            }
        }
        return true;

    }

    public static bool EquivalentMembers<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        return first.ContainsAll(second) && second.ContainsAll(first);
    }

    public static string GetCardNames(this IEnumerable<AbstractCard> cards)
    {
        var cardNames = cards.Select(item => item.Name).ToList();
        var aggregated = cardNames.Aggregate((item1, item2) => item1 + ", " + item2);
        return $"[{aggregated}]";
            
    }

    public static List<T> DedupeAndReorder<T>(this IEnumerable<T> toDedupe)
    {
        return toDedupe.ToHashSet().ToList();
    }


    public static List<T> SelectRandomFraction<T>(this IEnumerable<T> source, float percentageAsFraction)
    {
        if (percentageAsFraction > 1 || percentageAsFraction < 0)
        {
            throw new Exception("Required to express random fraction as a number between 0 and 1");
        }
        var list = new List<T>();
        foreach(var item in source)
        {
            if (UnityEngine.Random.value < percentageAsFraction)
            {
                list.Add(item);
            }
        }
        return list;
    }

    #region object pools
    public static Transform Spawn(this GameObject transform)
    {
        return GameObject.Instantiate(transform).transform;
        //return GetSpawnPool().Spawn(transform.transform);
    }
    public static void Despawn(this GameObject transform)
    {
        GameObject.Destroy(transform);
        //GetSpawnPool().Despawn(transform.transform);
    }

    public static Transform Spawn(this GameObject transform, Transform parent)
    {
        return GameObject.Instantiate(transform, parent).transform;
        // return GetSpawnPool().Spawn(transform.transform, parent);
    }

    public static Transform Spawn(this GameObject transform, Vector3 position, Quaternion rotation)
    {
        
        return GameObject.Instantiate(transform, position, rotation).transform;
        //return GetSpawnPool().Spawn(transform.transform, position, rotation);
    }

    public static string AsString<T>(this IEnumerable<T> list, Func<T, string> stringifyFunction)
    {
        if (list == null)
        {
            return "null";
        }

        var ret = "{";
        foreach (var item in list)
        {
            ret += stringifyFunction(item) + ", ";
        }
        ret += "}";
        return ret;
    }
    public static string AsString<T>(this IEnumerable<T> list)
    {
        if (list == null)
        {
            return "null";
        }

        var ret = "{";
        foreach(var item in list)
        {
            ret += item.ToString() + ", ";
        }
        ret += "}";
        return ret;
    }
    public static void SetToAbstractCardAttributes(this Card hyperCard, AbstractCard abstractCard)
    {
        if (abstractCard == null)
        {
            return;
        }
        hyperCard.SetCardTitle(abstractCard.Name);
        hyperCard.SetCardDescription(BuildTextBoxStringForCard(abstractCard));
        var ownerName = abstractCard.Owner?.CharacterFullName ?? "Communal";
        hyperCard.SetCardTopText($"{abstractCard.CardType}");

        hyperCard.SetCardEnergyCost(abstractCard.GetDisplayedEnergyCost());
        hyperCard.LogicalCardId = abstractCard.Id;
        hyperCard.LogicalCard = abstractCard;
        Image toEnable;
        List<Image> mutuallyExclusiveCardFrames = new List<Image>
        {
            hyperCard.CommonCardFrame,
            hyperCard.UncommonCardFrame,
            hyperCard.RareCardFrame,
            hyperCard.PurpleCardFrame,
            hyperCard.RedCardFrame
        };

        if (abstractCard.Rarity == Rarity.COMMON || abstractCard.Rarity == Rarity.BASIC || abstractCard.Rarity == Rarity.NOT_IN_POOL)
        {
            toEnable = hyperCard.CommonCardFrame;
        }
        else if (abstractCard.Rarity == Rarity.UNCOMMON)
        {

            toEnable = hyperCard.UncommonCardFrame;
        }
        else if (abstractCard.Rarity == Rarity.RARE)
        {
            toEnable = hyperCard.RareCardFrame;
        }
        else
        {
            toEnable = hyperCard.PurpleCardFrame;
        }

        foreach(var image in mutuallyExclusiveCardFrames)
        {
            if (image != toEnable)
            {
                image.gameObject.SetActive(false);
            }
            else
            {
                image.gameObject.SetActive(true);
            }
            
        }
    }

    private static string BuildTextBoxStringForCard(AbstractCard abstractCard)
    {
        return abstractCard.DescriptionInner();
    }

    public static T Spawn<T>(this T item) where T: MonoBehaviour
    {
        return item.gameObject.Spawn().GetComponent<T>();
    }

    public static T Spawn<T>(this T item, Transform transform) where T : MonoBehaviour
    {
        return item.gameObject.Spawn(transform).GetComponent<T>();
    }

    public static void AddToFront<T>(this List<T> items, T item)
    {
        var tempList = new List<T>();
        tempList.Add(item);
        tempList.AddRange(items);
        items.Clear();
        items.AddRange(tempList);
    }

    public static void SetText(this CustomGuiText item, string newText)
    {
        item.SetText(newText);
    }

    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
    {
        var set = new HashSet<T>();
        foreach(var item in source)
        {
            set.Add(item);
        }
        return set;

    }

    public static List<T> WhereNotNull<T>(this IEnumerable<T> itemCollection)
    {
        return itemCollection.Where(item =>item != null).ToList();
    }

    public static List<T> ToSingletonList<T>(this T item)
    {
        if (item == null)
        {
            return new List<T>();
        }
        return new List<T> { item };
    }

    #endregion
}