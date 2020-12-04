using UnityEngine;
using System.Collections;

/// <summary>
/// These represent PERSISTENT perks, as opposed to status effects, which are just for the combat.
/// Also corresponds to status effects.
/// Mostly stackable.
/// </summary>
public abstract class SoldierPerk
{
    public int Stacks { get; set; } = 1;
    public string Name { get; set; } = "unnamed";
    public ProtoGameSprite Sprite { get; set; } = ProtoGameSprite.Default;

    public virtual void PerformAtBeginningOfCombat(AbstractBattleUnit soldierAffected)
    {

    }

    public virtual void PerformAtBeginningOfNewDay(AbstractBattleUnit soldierAffected)
    {

    }

    public void AddStacks(AbstractBattleUnit soldierAffected, int stacks)
    {
        Stacks += stacks;
        if (Stacks <= 0)
        {
            soldierAffected.RemoveSoldierPerkByType(this.GetType());
        }
    }

    public void DecrementStacks(AbstractBattleUnit soldierAffected)
    {
        Stacks--;
        if (Stacks <= 0)
        {
            soldierAffected.RemoveSoldierPerkByType(this.GetType());
        }
    }
}
