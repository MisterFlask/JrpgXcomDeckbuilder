using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Mission 
{
    public string Name { get; set; }
    public int Difficulty { get; set; }

    public void OnFailed()
    {

    }

    public void OnSuccess()
    {

    }

    public bool IsFailed()
    {
        return false;
    }
    public void OnStartOfBattle()
    {

    }

    public List<AbstractBattleUnit> StartingEnemies()
    {
        return new List<AbstractBattleUnit>();
    }

    public int MaxNumberOfFriendlyCharacters { get; set; } = 4;
}
