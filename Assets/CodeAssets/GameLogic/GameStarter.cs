using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HyperCard;
using System;
using System.Linq;

public class GameStarter : TopoMonobehavior
{
    public GameStarter() : base(nameof(GameStarter))
    {
        Dependencies.Add(nameof(CardInstantiator));
        Dependencies.Add(nameof(CardAnimationManager));
        Dependencies.Add(nameof(GameState));
    }

    public void StartGame()
    {
        ServiceLocator.GetGameStateTracker().Deck = new Deck();
        var actionManager = ServiceLocator.GetActionManager();

        ServiceLocator.GetActionManager().EndTurn();
    }


    public override void Initialize()
    {
        StartGame();
    }

    public override void InnerStart()
    {
    }

    public override void InnerUpdate()
    {
    }
}
