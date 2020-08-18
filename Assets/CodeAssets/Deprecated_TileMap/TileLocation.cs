using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;

public class TileLocation
{
    public TileLocation()
    {

    }
    public TileLocation(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
    public int X { get; set; }
    public int Y { get; set; }

    public override bool Equals(object obj)
    {
        return obj is TileLocation location &&
               X == location.X &&
               Y == location.Y;
    }
    public override int GetHashCode()
    {
        var hashCode = 1861411795;
        hashCode = hashCode * -1521134295 + X.GetHashCode();
        hashCode = hashCode * -1521134295 + Y.GetHashCode();
        return hashCode;
    }

    public override string ToString()
    {
        return $"[{X}, {Y}]";
    }

    public bool IsNeighbor(TileLocation other)
    {
        return DistanceToTile(other) == 1;
    }

    public int DistanceToTile(TileLocation goal)
    {
        var dx = Math.Abs(goal.X - this.X);
        var dy = Math.Abs(goal.Y - this.Y);
        return dx + dy;
    }

    public Vector3 GetWorldCoordinatesOfTile()
    {
        var loc = ServiceLocator.GetTileMap().TilesByLocation[this];
        return loc.HexPrefab.transform.position;
    }

    public TileLocation Plus(TileLocation other)
    {
        return new TileLocation(this.X + other.X, this.Y + other.Y);
    }

    private static List<TileLocation> CardinalDirections()
    {
        return new List<TileLocation>
        {
            new TileLocation(0, 1),
            new TileLocation(1, 0),
            new TileLocation(-1, 0),
            new TileLocation(0, -1)
        };
    }

    public AbstractRivalUnit UnitHere()
    {
        return ServiceLocator.GetGameStateTracker().RivalUnits.Where(item => item.TileLocation == this)
            .FirstOrDefault();
    }

    public TileLocation GetNeighboringTileOrSelfClosestTo(TileLocation goal)
    {
        var neighbors = GetNeighbors().ToList();
        neighbors.Add(this);
        var closest = neighbors.OrderBy(item => item.DistanceToTile(goal))
            .Where(item => item.UnitHere() == null || item == this)
            .First();
        return closest;
    }

    public TileLocation ClosestTileOwnedByPlayer()
    {
        var allTilesOwnedByPlayer = ServiceLocator.GetTileMap().GetPlayerOwnedTiles();
        return allTilesOwnedByPlayer.OrderBy(item => item.TileLocation.DistanceToTile(this))
            .First().TileLocation;
    }

    public IEnumerable<TileLocation> GetNeighbors()
    {
        return CardinalDirections()
            .Select(item => item.Plus(this))
            .Where(item => item.Exists())
            .ToList();
    }

    public bool Exists()
    {
        return ServiceLocator.GetTileMap().TilesByLocation.Keys.Contains(this);
    }

    public Dictionary<CardinalDirection, TileLocation> GetNeighborsByCardinal()
    {
        var cardinals = new Dictionary<CardinalDirection, TileLocation>();
        cardinals.Add(CardinalDirection.LEFT, this.Plus(LEFT));
        cardinals.Add(CardinalDirection.RIGHT, this.Plus(RIGHT));
        cardinals.Add(CardinalDirection.UP, this.Plus(UP));
        cardinals.Add(CardinalDirection.DOWN, this.Plus(DOWN));
        return cardinals.Where(item => item.Value.Exists()).ToDictionary(item => item.Key, item => item.Value);
    }

    public static TileLocation LEFT = new TileLocation(-1, 0);
    public static TileLocation RIGHT = new TileLocation(1, 0);
    public static TileLocation UP = new TileLocation(0, 1);
    public static TileLocation DOWN = new TileLocation(0, -1);
}
public enum CardinalDirection
{
    LEFT,
    RIGHT,
    UP,
    DOWN
}