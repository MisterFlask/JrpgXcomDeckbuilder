using UnityEngine;
using System.Collections;
using System;

public static class OryxSprites
{
    private const string CharactersPathPrefix = "Sprites/Characters/oryx_16bit_scifi_creatures_";

    public static string CharactersPath(string id)
    {
        return CharactersPathPrefix + id;
    }

    public static ProtoGameSprite ProtoGameSpriteFromOryxCharacterSprite(
        string pathSuffix = "01",
        Color? color = null)
    {
        return new GameIconProtoSprite { Color = color ?? Color.white, SpritePath = CharactersPath(pathSuffix) };
    }


    /// <summary>
    /// This is being made so we can look at the column (0-indexed) and row (0-indexed) of the character grid from Oryx.
    /// </summary>
    public static ProtoGameSprite SelectRandomCharacterSpriteWithRandomColoration(int offset = 0)
    {
        offset++; //we do this because it's 1-indexed in the actual images.
        var random = UnityEngine.Random.Range(0, HumanRows);
        var index = random * SpritesPerRow + offset;

        string indexAsString = "" + index;
        if (indexAsString.Length == 1)
        {
            indexAsString = "0" + indexAsString;
        }
        return ProtoGameSpriteFromOryxCharacterSprite(indexAsString, GetRandomColoration());
    }

    private static Color GetRandomColoration()
    {
        var rOffset = UnityEngine.Random.Range(80, 100);
        var rOffsetToUnit = rOffset * .01;

        var gOffset = UnityEngine.Random.Range(80, 100);
        var gOffsetToUnit = gOffset * .01;

        var bOffset = UnityEngine.Random.Range(80, 100);
        var bOffsetToUnit = bOffset * .01;

        return new Color((float)rOffsetToUnit, (float)gOffsetToUnit, (float)bOffsetToUnit);
    }

    public static int SpritesPerRow = 32; // All of these correspond to different items a particular unit might be carrying.
    public static int HumanRows = 8; // first 8 rows are for humans.

}
