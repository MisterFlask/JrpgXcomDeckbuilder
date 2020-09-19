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

    public string Name { get; set; }
    public abstract string Description { get; }
    public AbstractBattleUnit OwnerUnit { get; set; }
    public bool Stackable { get; set; } = false;
    public int Stacks { get; set; } = 1;
    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon();

    public virtual void OnTurnStart()
    {
        
    }

    public virtual void OnDeath()
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

    public BattleUnitAttributePrefab CorrespondingPrefab { get; set; }

}
