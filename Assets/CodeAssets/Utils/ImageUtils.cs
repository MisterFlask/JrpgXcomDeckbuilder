using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ImageUtils
{
    public const string MeepleImagePath = "Sprites/3d-meeple";
    public const string CrossedSwordsImagePath = "Sprites/crossed-swords";


    private static string DefenseImage = "stone-tower";


    public static ProtoGameSprite ProtoGameSpriteFromGameIcon(
        string path = MeepleImagePath,
        Color? color = null)
    {
        return new GameIconProtoSprite { Color = color ?? Color.white, SpritePath = path };
    }

    public static Sprite LoadSprite(string imageName)
    {
        var loaded = Resources.Load<Sprite>(imageName);
        if (loaded == null)
        {
            Debug.LogError("Could not load sprite from image path: " + imageName);
            loaded = Resources.Load<Sprite>(imageName);
        }
        return loaded;
    }

}

public abstract class ProtoGameSprite
{
    public abstract GameSprite ToGameSpriteImage();

    public Sprite ToSprite()
    {
        return ToGameSpriteImage().Sprite;
    }
}

public class GameSprite
{
    public Sprite Sprite { get; set; }
    public Color Color { get; set; }
}

public class GameIconProtoSprite: ProtoGameSprite
{
    public string SpritePath { get; set; }
    public Color Color { get; set; }

    public override GameSprite ToGameSpriteImage()
    {
        var loaded = Resources.Load<Sprite>(SpritePath);
        if (loaded == null)
        {
            Debug.LogError("Could not load sprite from image path: " + SpritePath + "; using meeple instead");
            loaded = Resources.Load<Sprite>(ImageUtils.MeepleImagePath);
        }

        return new GameSprite
        {
            Color = Color,
            Sprite = loaded
        };
    }
}