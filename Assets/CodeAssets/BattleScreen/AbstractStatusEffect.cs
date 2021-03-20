﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.CodeAssets.GameLogic;
using System;

public abstract class AbstractStatusEffect: MagicWord
{
    #region convenience functions
    public ActionManager action()
    {
        return ServiceLocator.GetActionManager();
    }

    public List<AbstractBattleUnit> enemies()
    {
        return ServiceLocator.GetGameStateTracker().EnemyUnitsInBattle;
    }
    public List<AbstractBattleUnit> allies()
    {
        return ServiceLocator.GetGameStateTracker().AllyUnitsInBattle;
    }

    public GameState state()
    {
        return ServiceLocator.GetGameStateTracker();
    }
    #endregion
    public StatusLocalityType StatusLocalityType = StatusLocalityType.UNIT;

    //This is used for keeping track of stuff like "how many cards have been played"
    public int? InternalCounter = null;
    public StatusPolarityType StatusPolarityType { get; set; } = StatusPolarityType.NEITHER; 
    public AbstractCard ReferencedCard { get; set; }
    public override string MagicWordTitle => this.Name;
    public override string MagicWordDescription => this.Description;

    public bool AllowedToGoNegative = false;
    public string Name { get; set; }
    public abstract string Description { get; }
    public AbstractBattleUnit OwnerUnit { get; set; }
    public bool Stackable { get; set; } = true;
    public int Stacks { get; set; } = 1;

    public bool IsExample { get; set; }

    public string DisplayedStacks()
    {
        if (IsExample)
        {
            return "[stacks]";
        }
        else
        {
            return $"{Stacks}";
        }
    }

    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon();

    public AbstractStatusEffect()
    {
        Name = GetType().Name;
    }

    public virtual void OnTurnEnd()
    {
        
    }

    public virtual void OnDeath(AbstractBattleUnit unitThatKilledMe)
    {

    }

    public virtual void OnStruck(AbstractBattleUnit unitStriking, int totalDamageTaken)
    {

    }

    public virtual void OnStriking(AbstractBattleUnit unitStruck, int totalDamageDealt)
    {

    }

    public virtual int DamageDealtAddition()
    {
        return 0;
    }

    public virtual int DamageReceivedAddition()
    {
        return 0;
    }

    public virtual float DamageDealtIncrementalMultiplier()
    {
        return 0;
    }

    public virtual float DamageReceivedIncrementalMultiplier()
    {
        return 0;
    }


    public virtual int DefenseDealtAddition()
    {
        return 0;
    }

    public virtual int DefenseReceivedAddition()
    {
        return 0;
    }

    public virtual float DefenseDealtIncrementalMultiplier()
    {
        return 0;
    }

    public virtual float DefenseReceivedIncrementalMultiplier()
    {
        return 0;
    }

    public void AssignOwner(AbstractBattleUnit unit)
    {
        if (OwnerUnit != null)
        {
            throw new System.Exception("Cannot reassign status effects.");
        }
        OwnerUnit = unit;
    }

    public AbstractStatusEffect CloneStatusEffect()
    {
        return (AbstractStatusEffect) this.MemberwiseClone();
    }

    public virtual void OnDealingUnblockedDamage()
    {

    }

    public virtual void OnApplicationOrIncrease()
    {

    }

    // Returns the number of stacks that are to be applied, after processing.
    public virtual int OnAnyStatusEffectApplicationToOwner(AbstractStatusEffect statusEffectApplied, int stacksAppliedOrDecremented)
    {
        return stacksAppliedOrDecremented;
    }


    public virtual void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard)
    {

    }
    public virtual void OnAnyCardDrawn(AbstractCard cardDrawn)
    {

    }

    public BattleUnitAttributePrefab CorrespondingPrefab { get; set; }

    /// <summary>
    /// Note that this BOTH modifies the damage AND does anything relating to the damage modification (such as decreasing stacks of the mitigating attribute.)
    /// This is expected to mitigate damage AFTER block is consumed.  PRE-BLOCK damage is not impacted by this method.
    /// </summary>
    /// <param name="damageBlob"></param>
    public virtual void ModifyPostBlockDamageTaken(DamageBlob damageBlob)
    {

    }

    public virtual void ProcessProc(AbstractProc proc)
    {

    }

}

public enum StatusEffectChange
{
    INCREASE,DECREASE
}

public enum StatusPolarityType
{
    BUFF,
    DEBUFF,
    NEITHER
}

public enum StatusLocalityType
{
    UNIT,
    GLOBAL
}