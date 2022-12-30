using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

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

    public Texture ToTexture()
    {
        return ToSprite().SpriteToTexture();
    }

    // convenience methods follow
    public static ProtoGameSprite Default => ImageUtils.ProtoGameSpriteFromGameIcon();

    public string SpritePath { get; set; }

    public static ProtoGameSprite FromGameIcon(
        string path = ImageUtils.MeepleImagePath,
        Color? color = null)
    {
        return ImageUtils.ProtoGameSpriteFromGameIcon(path, color);
    }

    public static ProtoGameSprite CogIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Cog/" + name);
    }
    public static ProtoGameSprite BlackhandIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Blackhand/" + name);
    }
    public static ProtoGameSprite DiabolistIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Diabolist/" + name);
    }
    public static ProtoGameSprite ArchonIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Archon/" + name);
    }
    public static ProtoGameSprite SifterIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Sifter/" + name);
    }
    public static ProtoGameSprite HammerIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Hammer/" + name);
    }
    public static ProtoGameSprite RookieIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Rookie/" + name);
    }

    internal static ProtoGameSprite TerrainIcon(string v)
    {
        return FromGameIcon("Sprites/MissionTerrain/" + v);
    }

    public static ProtoGameSprite OtherIcons(string name)
    {
        return FromGameIcon("Sprites/OtherIcons/" + name);
    }
    public static ProtoGameSprite AttributeOrAugmentIcon(string name)
    {
        return FromGameIcon("Sprites/AttributesAndAugments/" + name);
    }
    public static ProtoGameSprite MadnessIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Madness/" + name);
    }
    public static ProtoGameSprite VisualTagIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/CardVisualTags/" + name);
    }

    internal static ProtoGameSprite StatusCardIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/StatusCards/" + name);

    }

    internal static ProtoGameSprite EmblemIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/ClassEmblems/" + name);
    }
    public static ProtoGameSprite MissionIcon(string name)
    {
        return FromGameIcon("Sprites/MissionIcons/" + name);
    }

    internal static ProtoGameSprite MachineBattler(string name)
    {
        return FromGameIcon("Sprites/Enemies/Machines/" + name);
    }
    internal static ProtoGameSprite MapIcon(string name)
    {
        return FromGameIcon("Sprites/MapSprites/" + name);
    }
}

public static class ImageExtensions
{

    public static Texture SpriteToTexture(this Sprite sprite)
    {
        return  Texture2D.CreateExternalTexture(
            (int)sprite.rect.width,
            (int)sprite.rect.height,
            TextureFormat.RGBA32,
            false,
            false,
            sprite.texture.GetNativeTexturePtr()
        );
    }
    
    public static void SetProtoSprite(this Image image, ProtoGameSprite protoSprite)
    {
        image.sprite = protoSprite.ToSprite();
        image.color = protoSprite.ToGameSpriteImage().Color;

    }

    /// <summary>
    /// attempts to keep same sprite size
    /// </summary>
    /// <param name="image"></param>
    /// <param name="protoSprite"></param>
    public static void SetProtoSprite(this SpriteRenderer image, ProtoGameSprite protoSprite)
    {
        var originalSpriteSize = image.sprite.rect;

        image.sprite = protoSprite.ToSprite();
        image.color = protoSprite.ToGameSpriteImage().Color;

        var newSpriteSize = image.sprite.rect;

        var scaleFactorWidth = originalSpriteSize.width / newSpriteSize.width;
        var scaleFactorHeight = originalSpriteSize.height / newSpriteSize.height;

        image.transform.localScale = new Vector3(scaleFactorWidth * image.transform.localScale.x, scaleFactorHeight * image.transform.localScale.y, image.transform.localScale.z);
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

    public static List<string> GetImagesPathsInFolderRecursively(String folderName)
    {

        DirectoryInfo Folder;
        FileInfo[] Images;
        var files = new List<string>();

        Folder = new DirectoryInfo(folderName);
        Images = Folder.GetFiles();

        foreach(var file in Folder.GetFiles())
        {
            // if file ends with jpg, add it to list
        }

        // foreach folder, get images paths


        var imagesList = new List<string>();

        for (int i = 0; i < Images.Length; i++)
        {
            imagesList.Add(string.Format(@"{0}/{1}", folderName, Images[i].Name));
        }

        return imagesList;
    }

}