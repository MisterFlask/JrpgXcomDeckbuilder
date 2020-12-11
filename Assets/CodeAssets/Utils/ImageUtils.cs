using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageUtils
{
    public const string MeepleImagePath = "Sprites/3d-meeple";
    public const string CrossedSwordsImagePath = "Sprites/crossed-swords";

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
            loaded = Resources.Load<Sprite>("Sprites/3d-meeple");
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

    // convenience methods follow
    public static ProtoGameSprite Default => ImageUtils.ProtoGameSpriteFromGameIcon();
    public static ProtoGameSprite FromGameIcon(
        string path = ImageUtils.MeepleImagePath,
        Color? color = null)
    {
        return ImageUtils.ProtoGameSpriteFromGameIcon(path, color);
    }
}

public static class ImageExtensions
{
    public static void SetProtoSprite(this Image image, ProtoGameSprite protoSprite)
    {
        image.sprite = protoSprite.ToSprite();
        image.color = protoSprite.ToGameSpriteImage().Color;

    }
}

public class GameSprite
{
    public Sprite Sprite { get; set; }
    public Color Color { get; set; }
}

public class GameIconProtoSprite: ProtoGameSprite
{
    public bool ReverseXAxis { get; set; } = false; //todo

    // todo: animation functionality
    //public List<string> AnimatedSpritePath = null;

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