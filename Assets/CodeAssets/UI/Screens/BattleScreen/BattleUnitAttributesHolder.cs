using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class BattleUnitAttributesHolder : MonoBehaviour
{
    public BattleUnitAttributePrefab AttributePrefabTemplate;

    public AbstractBattleUnit BattleUnit;

    public void Update()
    {
        if (BattleUnit == null)
        {
            this.PurgeChildren();
            return;
        }

        this.PurgeChildrenIf<BattleUnitAttributePrefab>(item => item.CorrespondingAttribute == null);

        var children = GetComponentsInChildren<BattleUnitAttributePrefab>().ToList();
        var displayedAttributes = children.Select(item => item.CorrespondingAttribute).WhereNotNull();
        var currentAttributes = BattleUnit.StatusEffects;
        foreach(var attr in currentAttributes)
        {
            if (!displayedAttributes.Any(item => item.GetType() == attr.GetType())){
                var newPrefab = AttributePrefabTemplate.Spawn(this.transform);
                newPrefab.Initialize(attr, this);
            }

            if (attr.Stacks <= 0 && attr.CorrespondingPrefab != null)
            {
                attr.CorrespondingPrefab.gameObject.Despawn();
                attr.CorrespondingPrefab = null;
            }
        }
    }

}