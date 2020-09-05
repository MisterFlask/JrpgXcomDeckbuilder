using UnityEngine;
using System.Collections;

public static class BattleStarter
{
    private static GameState state => ServiceLocator.GetGameStateTracker();
    public static void StartBattle(BattleScreenPrefab battleScreen)
    {
        state.EnemyUnitsInBattle.ForEach((item) => item.CurrentIntents = item.GetNextIntents());
        // First, we kick off the player's turn.
    } 
}
