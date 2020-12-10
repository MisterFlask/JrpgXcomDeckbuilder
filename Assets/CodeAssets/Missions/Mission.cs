using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Mission 
{
    public static string GenerateMissionName()
    {
        return "Operation " + WordLists.GetRandomCommonAdjective() + " " + WordLists.GetRandomCommonNoun();
    }
    public string Name { get; set; }
    public int Difficulty { get; set; } // 1 to 5

    //After this many days, the mission can no longer be performed.
    public int DaysUntilExpiration { get; set; } = 4;

    public List<AbstractMissionReward> Rewards { get; set; } = new List<AbstractMissionReward>();

    public virtual void OnFailed()
    {

    }

    public virtual void OnSuccess()
    {

    }

    public virtual bool IsFailed()
    {
        return GameState.Instance.AllyUnitsInBattle.TrueForAll(item => item.IsDead);
    }

    public virtual void OnStartOfBattle()
    {

    }

    public virtual List<AbstractBattleUnit> StartingEnemies()
    {
        return new List<AbstractBattleUnit>();
    }

    public int MaxNumberOfFriendlyCharacters { get; set; } = 3;

    public bool IsFailure { get; set; } = false;
    public bool IsVictory { get; set; } = false;
}
