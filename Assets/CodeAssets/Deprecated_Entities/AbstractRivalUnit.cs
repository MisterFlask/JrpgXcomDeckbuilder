using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbstractRivalUnit
{
    public int Power { get; set; } = 2;
    public int Toughness { get; set; } = 3;
    public string Name { get; set; } = "Unnamed";

    public Sprite Sprite { get; set; }

    public TileLocation TileLocation { get; set; }
    public Faction Faction { get; set; }


    public AbstractRivalUnit Clone()
    {
        var clone = MemberwiseClone();
        return clone as AbstractRivalUnit;
    }
}
