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
        Dependencies.Add(nameof(TileMap));
    }

    public void StartGame()
    {
        ServiceLocator.GetGameStateTracker().Factions = Faction.RivalFactionsList;

        EntityRegistrar.RegisterMissions();
        EntityRegistrar.RegisterCards();

        ServiceLocator.GetGameStateTracker().Deck = new Deck();
        var actionManager = ServiceLocator.GetActionManager();

        actionManager.AssignStartingTilesAndUnitsForEachFaction();

        // add starting deck!
        foreach (var card in GetStartingCards())
        {
            actionManager.AddCardToDeck(card);
        }

        // Now, we assign defense values to all tiles
        var tilemap = ServiceLocator.GetTileMap();

        ServiceLocator.GetActionManager().EndTurn();
    }

    private int GetStartingHexDefense(LogicalTile tile)
    {
        return 2;
    }


    private IEnumerable<AbstractCard> GetStartingCards()
    {

        return new List<AbstractCard>
        {
            new Laborers(),
            new Pilgrims(),
            new ElderCouncil(),
            new Laborers(),
            new Pilgrims(),
            new Palace()
        };

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
