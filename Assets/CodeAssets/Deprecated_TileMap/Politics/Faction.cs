using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Faction
{
    public TileLocation FactionBase { get; set; }

    public string Name { get; set; }

    public Color color { get; set; }

    public int BaseProgress { get; set; } // Total progress = # of territories controlled + base progress

    public bool IsPlayer { get; set; } = false;

    public static Faction TheLunarCollective = new Faction
    {
        Name = "The Lunar Collective",
        color = Color.cyan
    };
    public static Faction TheBrimstoneCaucus = new Faction
    {
        Name = "The Brimstone Caucus",
        color = Color.red
    };
    public static List<Faction> RivalFactionsList = new List<Faction> {
         
        TheLunarCollective,
        TheBrimstoneCaucus
    };


    public static Faction WildernessFaction = new Faction { Name = "Wilderness", color = Color.white };
    public static Faction PlayerFaction = new Faction { Name = "Player", color = Color.green, IsPlayer = true };

    public static Faction GetPlayerFaction()
    {
        return PlayerFaction;
    }

    public int NumTerritoriesControlled()
    {
        return GetTerritories().Count();

    }
    public List<LogicalTile> GetTerritories()
    {
        try 
        { 
            var tiles = ServiceLocator.GetTileMap().TilesByLocation.Values;
            return tiles.Where(item => item.Owner == this).ToList();
        }
        catch(Exception e)
        {
            //here for the sake of debugging
            throw;
        }
    }


    public int TotalProgress()
    {
        return NumTerritoriesControlled() + BaseProgress;
    }
    
}
