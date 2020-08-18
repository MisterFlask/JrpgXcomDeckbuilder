using UnityEngine;
using System.Collections;

public class TileMapUtils : MonoBehaviour
{

    public static float tile_width = 2.56f;
    public static float tile_height = 2.56f; // sprite is 3.84f total
    public static float tile_under_height = 1.28f;
    public static float anchor_x = 0;
    public static float anchor_y = 0;
    public static int numTiles = 20;
    public static Sprite LoadTerrain(string name)
    {
        return Resources.Load<Sprite>($"Terrain Hexes/Tiles/{name}");
    }

    public static void SetTerrainType(TileLocation tileLocation, TerrainType terrainType)
    {
        // no op, for now
    }

    public static void SetAnchors(float anchorx, float anchory)
    {
        anchor_x = anchorx;
        anchor_y = anchory;
    }

    public static void SetNumTiles(int tiles)
    {
        numTiles = tiles;
    }

    public static Vector3 GetWorldLocationOfHexTile(TileLocation location)
    {
        var pos_x = location.X * tile_width;
        var pos_y = location.Y * tile_height * .75f; // I don't know why we do this, it's that way in reference impl
        if (location.Y % 2 == 0)
        {
            // offset alternate rows by half width
            pos_x += tile_width * 0.5f;
        }
        return new Vector3(pos_x, pos_y, Constants.TERRAIN_Z);
    }
    public static int GetSortOrderOfTile(TileLocation location)
    {
        return -1 * location.Y - numTiles;
    }



}
