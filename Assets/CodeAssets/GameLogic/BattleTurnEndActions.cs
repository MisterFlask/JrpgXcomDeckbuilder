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
        ActionManager.Instance.DoAThing(() => 
        {
            gameState.AllyUnitsInBattle.ForEach(item => item.OnTurnEnd());
        });

        ActionManager.Instance.DoAThing(() =>
        {
            gameState.EnemyUnitsInBattle.ForEach(item => item.OnTurnEnd());
        });

        gameState.EnemyUnitsInBattle.ForEach(item => item.ExecuteOnIntentIfAvailable());

        foreach(var card in GameState.Instance.Deck.TotalDeckList)
        {
            card.TemporaryCostMod = 0; // these get reset to 0 at the beginning of each turn.
        }

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
