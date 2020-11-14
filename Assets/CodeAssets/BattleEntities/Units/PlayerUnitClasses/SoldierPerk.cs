using UnityEngine;
using System.Collections;

/// <summary>
/// These represent PERSISTENT perks, as opposed to status effects, which are just for the combat.
/// </summary>
public abstract class SoldierPerk
{

    public string Name { get; set; } = "unnamed";
    public ProtoGameSprite Sprite { get; set; } = ProtoGameSprite.Default;

    public virtual void PerformAtBeginningOfCombat()
    {

    }

    public virtual void PerformAtBeginningOfNewDay()
    {

    }
}
