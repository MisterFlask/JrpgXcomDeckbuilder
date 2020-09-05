using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class Intent
{
    public Intent(AbstractBattleUnit source, List<AbstractBattleUnit> unitsTargeted = null)
    {
        this.Source = source;
        this.UnitsTargeted = unitsTargeted ?? new List<AbstractBattleUnit>();
    }

    public string Id = Guid.NewGuid().ToString();

    public abstract void Execute();

    protected abstract IntentPrefab GeneratePrefab(GameObject parent);

    public IntentPrefab GeneratePrefabAndAssign(GameObject parent)
    {
        var prefab = GeneratePrefab(parent);
        prefab.UnderlyingIntent = this;
        return prefab;
    }

    // can be an empty list
    public List<AbstractBattleUnit> UnitsTargeted { get; set; } = new List<AbstractBattleUnit>();

    public AbstractBattleUnit Source { get; set; }
}