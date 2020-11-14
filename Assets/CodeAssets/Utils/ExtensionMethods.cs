
using HyperCard;
using PathologicalGames;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionMethods
{

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
            UnityEngine.Object.Destroy(obj.transform.GetChild(i));
        }
    }
    public static void PurgeChildren(this GameObject obj)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            UnityEngine.Object.Destroy(obj.transform.GetChild(i));
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

    #region object pools
    public static Transform Spawn(this GameObject transform)
    {
        return GetSpawnPool().Spawn(transform.transform);
    }
    public static void Despawn(this GameObject transform)
    {
        GetSpawnPool().Despawn(transform.transform);
    }

    public static Transform Spawn(this GameObject transform, Transform parent)
    {
        return GetSpawnPool().Spawn(transform.transform, parent);
    }

    public static Transform Spawn(this GameObject transform, Vector3 position, Quaternion rotation)
    {
        return GetSpawnPool().Spawn(transform.transform, position, rotation);
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
        var ownerName = abstractCard.Owner?.CharacterName ?? "Communal";
        hyperCard.SetCardTopText($"{ownerName} | {abstractCard.CardType}");

        hyperCard.SetCardEnergyCost(BattleRules.CalculateEnergyCost(abstractCard));
        hyperCard.LogicalCardId = abstractCard.Id;
        hyperCard.LogicalCard = abstractCard;
    }

    private static string BuildTextBoxStringForCard(AbstractCard abstractCard)
    {
        return abstractCard.Description();
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