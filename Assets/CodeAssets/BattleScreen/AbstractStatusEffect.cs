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

    public virtual int DamageDealtMod()
    {
        return 0;
    }
    public virtual int DamageReceivedMod()
    {
        return 0;
    }

    public BattleUnitAttributePrefab CorrespondingPrefab { get; set; }

}
