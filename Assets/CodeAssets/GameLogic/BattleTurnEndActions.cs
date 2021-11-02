using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class BattleTurnEndActions
{
    ActionManager actionManager => ServiceLocator.GetActionManager();
    GameState gameState => ServiceLocator.GameState();

    internal void EndTurn()
    {
        actionManager.DiscardHand();

        gameState.AllyUnitsInBattle.ForEachCreateAction(item => item.OnTurnEnd());

        gameState.EnemyUnitsInBattle.ForEachCreateAction(item => item.OnTurnEnd());

        gameState.EnemyUnitsInBattle.ForEachCreateAction(item => item.ExecuteOnIntentIfAvailable());

        GameState.Instance.Deck.TotalDeckList.ForEachCreateAction(item =>
        {
            item.RestOfTurnCostMod = 0; // these get reset to 0 at the beginning of each turn.
        });

        StartNewTurn();
    }

    private void StartNewTurn()
    {
        GameState.Instance.NumCardsPlayedThisTurn = 0;
        ServiceLocator.GameState().BattleTurn++;

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
            ServiceLocator.GameState().energy = ServiceLocator.GameState().maxEnergy;
        });
        ActionManager.Instance.CheckIsBattleOver();
    }
}
