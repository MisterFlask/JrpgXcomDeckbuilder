
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleEntityPrefab:MonoBehaviour
{
    public Image EnemyImage;
    public BattleUnitAttributesHolder BattleUnitAttributesHolder;

    public BattleEntity UnderlyingEntity { get; private set; }

    public void Initialize(BattleEntity entity)
    {
        UnderlyingEntity = entity;
    }
}

public class BattleUnitAttributesHolder: MonoBehaviour
{
    public void AddBattleAttribute(BattleUnitAttribute attr)
    {

    }
    public void RemoveBattleAttribute(BattleUnitAttribute attr)
    {

    }
}