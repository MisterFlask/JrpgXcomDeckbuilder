
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LogicalTile
{
    public TileLocation TileLocation { get; set; }
    public TerrainType TerrainType { get; set; } = TerrainType.Plains;
    public Faction Owner { get; set; } // can be null
    public FuzzyOutlineProperties FuzzyOutlineProperties { get; set; } = new FuzzyOutlineProperties();

    public int Power { get; set; } = 1;
    public int Toughness { get; set; } = 1;
    public GameTilePrefab HexPrefab { get; set; }

    public bool HiddenByFog { get; set; }

    public List<LogicalTile> GetNeighbors()
    {
        return this.TileLocation.GetNeighbors().Select(item => item.ToTile()).ToList();
    }
}
