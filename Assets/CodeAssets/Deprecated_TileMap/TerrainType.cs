using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainType
{

    public string Name { get; set; }

    public List<TerrainImage> ApplicableTileSprites { get; set; } = new List<TerrainImage>();

    public ResourceYields ResourceYields { get; set; } = new ResourceYields(0, 0, 0, 0);

    public ResourceYields UpgradeIncrementalResourceYields { get; set; } = new ResourceYields(0, 0, 0, 0);

    #region staticTerrains
    public static TerrainType Forest = new TerrainType
    {
        Name = "Forest",
        ApplicableTileSprites = new List<TerrainImage>{
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexForestBroadleaf02") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexForestBroadleaf03") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexForestBroadleaf00") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexForestBroadleaf01") }
            },
        ResourceYields = new ResourceYields
        {
            Commerce = 0,
            Production = 10,
            Science = 0,
            Food = 5
        },
        UpgradeIncrementalResourceYields = new ResourceYields
        {
            Production = 2,
            Food = 1
        }
    };

    public static TerrainType Plains = new TerrainType
    {
        Name = "Plains",
        ApplicableTileSprites = new List<TerrainImage>{
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexPlains02") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexPlains03") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexPlains00") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexPlains01") }
            },

        ResourceYields = new ResourceYields(10, 0, 0, 0),
        UpgradeIncrementalResourceYields = new ResourceYields
        {
            Food = 2
        }
    };

    public static TerrainType Water =
        new TerrainType
        {
            Name = "Water",
            ApplicableTileSprites = new List<TerrainImage>{
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexOcean02") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexOcean01") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexOcean03") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexOcean00") }
            },

            ResourceYields = new ResourceYields(5, 0, 0, 5),
            UpgradeIncrementalResourceYields = new ResourceYields
            {
                Commerce = 1,
                Food = 1
            }
        };
    public static TerrainType Desert = new TerrainType
    {
        Name = "Desert",
        ApplicableTileSprites = new List<TerrainImage>{
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexDesertDunes03") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexDesertDunes00") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexDesertDunes01") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexDesertDunes02") }
            },

        ResourceYields = new ResourceYields(0, 0, 0, 5),
        UpgradeIncrementalResourceYields = new ResourceYields
        {
            Commerce = 1
        }
    };

    public static TerrainType Mountain = new TerrainType { Name = "Mountain", ApplicableTileSprites = new List<TerrainImage>{
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexMountain00") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexMountain01") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexMountain02") },
            new TerrainImage{ ImageResourcePath = TerrainImageResource.GetBaseImage("hexMountain03") }
            },
        ResourceYields = new ResourceYields(0, 0, 10, 0),
        UpgradeIncrementalResourceYields = new ResourceYields
        {
            Production = 2
        }
    };
    #endregion



    public static List<TerrainType> StartingTerrainsForWorldGeneration = new List<TerrainType>
    {
            Forest ,Plains, Water, Desert, Mountain
    };
}

public class TerrainImage
{
    public string ImageResourcePath { get; set; }

    public Color Tinting { get; set; } = Color.white;

    public Sprite ToSprite()
    {
        var loaded = Resources.Load<Sprite>(ImageResourcePath) ;
        if (loaded == null)
        {
            Debug.LogError("Could not load sprite from image path: " + ImageResourcePath);
        }
        return loaded;
    }
}

public static class TerrainImageResource
{
    public const string ResourceBasePath = "Tiles/";
    public static string GetBaseImage(string endPath)
    {
        return ResourceBasePath + endPath;
    }

}

public class ResourceYields
{

    public ResourceYields()
    {

    }
    public ResourceYields(int food, int science, int production, int commerce)
    {
        Food = food;
        Science = science;
        Production = production;
        Commerce = commerce;
    }

    public string GetDisplayString(int genericResourceModifier = 0,
        int foodResourceModifier = 0,
        int scienceResourceModifier = 0,
        int productionResourceModifier = 0,
        int commerceResourceModifier = 0)
    {
        var returned = "";
        if (Food + foodResourceModifier != 0)
        {
            returned += $"Food: {Food + genericResourceModifier + foodResourceModifier}\n";
        }
        if (Science + scienceResourceModifier != 0)
        {
            returned += $"Science: {Science + genericResourceModifier + scienceResourceModifier}\n";
        }
        if (Production + productionResourceModifier != 0)
        {
            returned += $"Production: {Production + genericResourceModifier + productionResourceModifier}\n";
        }
        if (Commerce + commerceResourceModifier != 0)
        {
            returned += $"Commerce: {Commerce + genericResourceModifier + commerceResourceModifier}\n";
        }
        if (returned.IsEmpty())
        {
            return "Does nothing.";
        }
        return returned;
    }

    public int Food { get; set; } = 0;
    public int Science { get; set; } = 0;
    public int Production { get; set; } = 0;
    public int Commerce { get; set; } = 0;
}