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

    public void OnStartOfBattle()
    {

    }

    public List<BattleEntity> StartingEnemies()
    {
        return new List<BattleEntity>();
    }

    public int MaxNumberOfFriendlyCharacters { get; set; } = 4;
}
