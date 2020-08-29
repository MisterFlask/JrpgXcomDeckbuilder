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
        actionManager.DiscardHand();

        ServiceLocator.GetGameStateTracker().BattleTurn++;
        gameState.AllyUnitsInBattle.ForEach(item => item.OnTurnStart());
        gameState.EnemyUnitsInBattle.ForEach(item => item.OnTurnStart());

        actionManager.DrawCards(5);
    }
}
