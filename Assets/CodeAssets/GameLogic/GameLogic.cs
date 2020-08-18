using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameLogic
{
    List<AbstractCard> CardsAvailableForGame => EntityRegistrar.ResearchableCards;

    public GameLogic()
    {
    }

    public IEnumerable<AbstractCard> GetSelectableCardsFromScience()
    {
        return CardsAvailableForGame.PickRandom(3);
    }
    // Returns the list of attackable regions, which should be all regions VISIBLE that are NOT in player territory.

    public HashSet<TileLocation> GetAttackableRegions()
    {
        var tiles = ServiceLocator.GetTileMap().TilesByLocation.Values
            .Where(item => !Faction.GetPlayerFaction().GetTerritories().Contains(item));
        return tiles.Select(item => item.TileLocation).ToHashSet();
    }
    // Returns the list of conquerable regions, which should be all regions adjacent to the player territory that are NOT player territory.

    public HashSet<TileLocation> GetConquerableRegions()
    {
        var playerTiles = Faction.GetPlayerFaction().GetTerritories();
        var tilesNextToPlayerTiles = playerTiles.SelectMany(item => item.GetNeighbors());
        var tiles = ServiceLocator.GetTileMap().TilesByLocation.Values
            .Where(item => !playerTiles.Contains(item) && tilesNextToPlayerTiles.Contains(item));
        return tiles.Select(item => item.TileLocation).ToHashSet();
    }
}
