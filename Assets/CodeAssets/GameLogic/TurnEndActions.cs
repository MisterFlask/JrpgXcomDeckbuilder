using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class TurnEndActions
{
    ActionManager actionManager => ServiceLocator.GetActionManager();
    GameState gameState => ServiceLocator.GetGameStateTracker();

    internal void EndTurn()
    {
        actionManager.DiscardHand();
        actionManager.DrawCards(5);

        ServiceLocator.GetGameStateTracker().Turn++;
    }
}
