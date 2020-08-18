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

    public GameTilePrefab SquareTileImagePrefab;
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

    public void InitializeSquareTileMap()
    {
        var cameraObject = GameObject.FindObjectOfType<CameraController>();

        TilesByLocation = new Dictionary<TileLocation, LogicalTile>();
        var maxX = 10;
        var maxY = 10;
        var transform = SquareTileImagePrefab.transform as RectTransform;
        var tileWidth = transform.rect.width;
        var tileHeight = transform.rect.height;
        GameTilePrefab lastTileCreated = null;
        for (var x = 0; x < maxX; x++)
        {
            float currentPixelsX = tileWidth * x;
            for (var y = 0; y < maxY; y++)
            {
                float currentPixelsY = tileHeight * y;
                var thisTile = Instantiate(SquareTileImagePrefab);
                thisTile.transform.SetParent(this.transform);
                thisTile.transform.position = new Vector2(currentPixelsX, currentPixelsY);
                thisTile.TileLocation = new TileLocation(x, y);
                lastTileCreated = thisTile;
                TilesByLocation[thisTile.TileLocation] = new LogicalTile
                {
                    HiddenByFog = false,
                    Toughness = 1,
                    Power = 1,
                    Owner = null,
                    HexPrefab = thisTile,
                    TerrainType = TerrainType.Desert,
                    TileLocation = thisTile.TileLocation
                };
            }
        }
        cameraObject.ZoomToLookAt(lastTileCreated.gameObject);
    }

    public override void Initialize()
    {
        InitializeSquareTileMap();
    }
}