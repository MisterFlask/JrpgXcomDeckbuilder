using UnityEngine;
using System.Collections;

public static class TextGlyphs
{
    public static string CoinIcon = Icon("crown-coin");
    public static string StabilityIcon = Icon("stability");
    public static string ScienceIcon = Icon("earlenmeyer");
    public static string AttackIcon = "[P]";
    public static string ToughnessIcon = "[T]";

    private static string Icon(string value)
    {
        return $"<icon={value}>";
    }

    public static string Colored(Color c, string input)
    {
        return $"<color={ColorUtility.ToHtmlStringRGB(c)}>{input}</color>";
    }
    public static string Shake(string input)
    {
        return $"<anim=shake>{input}</anim>";
    }
    public static string Throb(string input)
    {
        return $"<anim=throb>{input}</anim>";
    }
    public static string Wave(string input)
    {
        return $"<anim=wave>{input}</anim>";
    }
}
