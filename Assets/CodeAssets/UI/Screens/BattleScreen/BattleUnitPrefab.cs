
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleUnitPrefab:MonoBehaviour
{
    public Image EnemyImage;
    public BattleUnitAttributesHolder BattleUnitAttributesHolder;

    public AbstractBattleUnit UnderlyingEntity { get; private set; }

    public void Initialize(AbstractBattleUnit entity)
    {
        UnderlyingEntity = entity;
    }
}

public class BattleUnitAttributesHolder: MonoBehaviour
{
    public void AddBattleAttribute(AbstractBattleUnitAttribute attr)
    {
        
    }
    public void RemoveBattleAttribute(AbstractBattleUnitAttribute attr)
    {

    }
}