using UnityEngine;
using System.Collections;
using System;

public class ImageUtils
{
    public static string StockPlaceholderImage = "3d-meeple";

    private static string TileDefenseImage = "stone-tower";

    public static Sprite GetTileDefenseImage()
    {
        return LoadSprite(TileDefenseImage);
    }

    public static Sprite LoadSprite(string imageName)
    {
        var loaded = Resources.Load<Sprite>(imageName);
        if (loaded == null)
        {
            Debug.LogError("Could not load sprite from image path: " + imageName);
            loaded = Resources.Load<Sprite>(StockPlaceholderImage);
        }
        return loaded;
    }

    public static Sprite GetFightingImage()
    {
        return LoadSprite("crossed-swords");
    }
}
