using UnityEngine;
using System.Collections;

public class CardTag
{
    public CardTag(string name, int quantity)
    {
        this.Name = name;
        this.Quantity = quantity;
    }
    public string Name { get; set; }
    public int Quantity { get; set; } = 1;

    public static string LIGHT = "Light";
    public static string NIGHT = "Night";
    public static string INFERNO = "Inferno";
    public static string SEA = "Sea";
    public static string RELIGIOUS = "Religious";
    public static string SCHOLA = "Schola";
    public override string ToString()
    {
        return $"{Name}: {Quantity}";
    }
}
