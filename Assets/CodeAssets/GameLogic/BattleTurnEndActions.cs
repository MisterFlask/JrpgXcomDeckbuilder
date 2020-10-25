using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class BattleTurnEndActions
{
    ActionManager actionManager => ServiceLocator.GetActionManager();
    GameState gameState => ServiceLocator.GetGameStateTracker();

    internal void EndTurn()
    {
        gameState.EnemyUnitsInBattle.ForEach(item => item.ExecuteOnIntentIfAvailable());
        actionManager.DiscardHand();
        StartNewTurn();
    }

    private void StartNewTurn()
    {
        ServiceLocator.GetGameStateTracker().BattleTurn++;

        ServiceLocator.GetActionManager().DoAThing(() =>
        {
            gameState.AllyUnitsInBattle.ForEach(item => item.OnTurnStart());
        });
        ServiceLocator.GetActionManager().DoAThing(() =>
        {
            gameState.EnemyUnitsInBattle.ForEach(item => item.OnTurnStart());
        });
        actionManager.DrawCards(5);
        ServiceLocator.GetActionManager().DoAThing(() =>
        {
            ServiceLocator.GetGameStateTracker().energy = ServiceLocator.GetGameStateTracker().maxEnergy;
        });
        ActionManager.Instance.CheckIsBattleOver();
    }
}
