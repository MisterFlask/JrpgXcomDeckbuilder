﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class AbstractBattleUnitAttribute
{
    public bool Stackable { get; set; } = false;
    public int Stacks { get; set; } = 1;
    public string ImageString { get; set; } = ImageUtils.MeepleImagePath;

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