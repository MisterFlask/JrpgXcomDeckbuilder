﻿using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// These represent PERSISTENT perks, as opposed to status effects, which are just for the combat.
/// Also corresponds to status effects.
/// Mostly stackable.
/// </summary>
public abstract class SoldierPerk
{
    public int Stacks { get; set; } = 1;
    public abstract string Name();
    public abstract string Description();

    public ProtoGameSprite Sprite { get; set; } = ProtoGameSprite.Default;
    public AbstractBattleUnit Owner { get; internal set; }

    public virtual void PerformAtBeginningOfCombat(AbstractBattleUnit soldierAffected)
    {

    }

    public virtual void PerformAtBeginningOfNewDay(AbstractBattleUnit soldierAffected)
    {

    }

    /// <summary>
    ///  This is run on the deck after the perk is gained, and also on every card as it enters the deck.
    /// </summary>
    public virtual void ModifyCardsUponAcquisition(AbstractCard card, AbstractBattleUnit soldierAffecte)
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


    public static SoldierPerk CreateGrantsStatusEffectPerk(
        string name,
        string description,
        AbstractStatusEffect effect,
        int stacks)
    {
        return CreateGrantsStatusEffectPerk(
        name,
        description,
        effect,
        stacks);
    }

    public virtual void OnAssignment(AbstractBattleUnit abstractBattleUnit)
    {

    }

    public SoldierPerk Clone()
    {
        var copy = (SoldierPerk)this.MemberwiseClone();
        return copy;
    }
}


public class GrantsStatusEffectPerk : SoldierPerk
{
    private string GivenName { get; set; }
    private string GivenDescription { get; set; }
    private AbstractStatusEffect Effect { get; set; }
    private Rarity Rarity { get; set; }
    public GrantsStatusEffectPerk(
        string name,
        string description,
        AbstractStatusEffect effect,
        int stacks = 1,
        Rarity rarity = Rarity.NOT_IN_POOL)
    {
        GivenName = name;
        GivenDescription = description;
        Stacks = stacks;
        Effect = effect;
        Rarity = rarity;
    }

    public override void PerformAtBeginningOfCombat(AbstractBattleUnit soldierAffected)
    {
        soldierAffected.ApplyStatusEffect(Effect, Stacks);
    }

    public override string Name()
    {
        return GivenName;
    }

    public override string Description()
    {
        return GivenDescription;
    }
}

