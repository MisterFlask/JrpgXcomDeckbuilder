using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class AbstractStatusEffect
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

    public bool AllowedToGoNegative = false;
    public string Name { get; set; }
    public abstract string Description { get; }
    public AbstractBattleUnit OwnerUnit { get; set; }
    public bool Stackable { get; set; } = true;
    public int Stacks { get; set; } = 1;
    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon();

    public AbstractStatusEffect()
    {
        Name = GetType().Name;
    }

    public virtual void OnTurnStart()
    {

    }

    public virtual void OnDeath(AbstractBattleUnit unitThatKilledMe)
    {

    }

    public virtual void OnStruck(AbstractBattleUnit unitStriking, int totalDamageTaken)
    {

    }

    public virtual void OnStriking(AbstractBattleUnit unitStriking, int totalDamageDealt)
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

    public virtual float DamageDealtMultiplier()
    {
        return 1;
    }

    public virtual float DamageReceivedMultiplier()
    {
        return 1;
    }


    public virtual int DefenseDealtAddition()
    {
        return 0;
    }

    public virtual int DefenseReceivedAddition()
    {
        return 0;
    }

    public virtual float DefenseDealtMultiplier()
    {
        return 1;
    }

    public virtual float DefenseReceivedMultiplier()
    {
        return 1;
    }

    public void AssignOwner(AbstractBattleUnit unit)
    {
        if (OwnerUnit != null)
        {
            throw new System.Exception("Cannot reassign status effects.");
        }
        OwnerUnit = unit;
    }

    public virtual void OnDealingUnblockedDamage()
    {

    }

    public virtual void OnApplicationOrIncrease()
    {

    }

    public virtual void OnAnyStatusEffectApplicationToOwner(StatusEffectChange increaseOrDecrease, AbstractStatusEffect statusEffectApplied)
    {

    }

    public virtual void OnAnyCardPlayed(AbstractCard cardPlayed)
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

}

public enum StatusEffectChange
{
    INCREASE,DECREASE
}
