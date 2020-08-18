using UnityEngine;
using System.Collections;

public class CardType
{
    string Name { get; set; }

    public CardType(string name)
    {
        this.Name = name;
    }

    public override string ToString()
    {
        return Name;
    }

    public static CardType LegionCard = new CardType("Legion");
    public static CardType TechCard = new CardType("Tech");
    public static CardType ConstructionCard = new CardType("Construction");

}
