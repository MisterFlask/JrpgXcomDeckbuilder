using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FogOfWarManager
{
    public void UpdateFogOfWar()
    {
        var visibleTileLocations = GetVisibleTiles();
        var tilemap = ServiceLocator.GetTileMap().TilesByLocation.Values;
        foreach(var tile in tilemap)
        {

            //tile.HexPrefab.FogOfWar.enabled = false;
            tile.HiddenByFog = false;
            /*
            if (visibleTileLocations.Contains(tile.TileLocation))
            {
                tile.HexPrefab.FogOfWar.enabled = false;
                tile.HiddenByFog = false;
            }
            else
            {
                tile.HexPrefab.FogOfWar.enabled = true;
                tile.HiddenByFog = true;
            }
            */
        }
    }

    public IEnumerable<TileLocation> GetVisibleTiles()
    {
        var tilesByLocation = ServiceLocator.GetTileMap().TilesByLocation;
        var tilesOwnedByPlayer = tilesByLocation.Values.Where(item => item.Owner?.IsPlayer ?? false).ToList();
        var tilesNearTilesOwnedByPlayer = tilesOwnedByPlayer
            .SelectMany(item => item.TileLocation.GetNeighbors())
            .Where(item => item != null)
            .ToList();
        return CollectionUtils.Aggregate<TileLocation>(tilesNearTilesOwnedByPlayer, tilesOwnedByPlayer.Select(item => item.TileLocation));
    }

}
