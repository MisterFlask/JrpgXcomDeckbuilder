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
        actionManager.DrawCards(5);

        ServiceLocator.GetGameStateTracker().BattleTurn++;
        gameState.PlayerCharactersInBattle.ForEach(item => item.OnTurnStart());
        gameState.EnemyUnitsInBattle.ForEach(item => item.OnTurnStart());
    }
}
