using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;

public class FuzzyOutlineProperties
{
    public Color BaseColor = new Color(1f, 1f, 1f, 0f);
}

public class TileMap : TopoMonobehavior
{
    public TileMap() : base(nameof(TileMap))
    {

    }

    public Sprite underground;
    public Dictionary<TileLocation, LogicalTile> TilesByLocation { get; private set; }

    public TileLocation GetCenterOfMap()
    {
        var keys = this.TilesByLocation.Keys; ;
        var highestX = keys.Max(item => item.X);
        var lowestX = keys.Min(item => item.X);
        var highestY = keys.Max(item => item.Y);
        var lowestY = keys.Min(item => item.Y);

        var avgX = (highestX + lowestX) / 2;
        var avgY = (highestY + lowestY) / 2;

        return new TileLocation(avgX, avgY);
    }

    void Start()
    {
        this.gameObject.AddComponent<GameStarter>();
    }

    public IEnumerable<LogicalTile> GetPlayerOwnedTiles()
    {
        return this.TilesByLocation.Values.Where(item => item.Owner != null && item.Owner.IsPlayer);
    }

    override public void InnerUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Name = " + hit.collider.name);
                Debug.Log("Tag = " + hit.collider.tag);
                Debug.Log("Hit Point = " + hit.point);
                Debug.Log("Object position = " + hit.collider.gameObject.transform.position);
                Debug.Log("--------------");
            }
        }
    }

    public override void Initialize()
    {

    }
}