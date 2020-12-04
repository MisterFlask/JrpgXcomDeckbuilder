using UnityEngine;
using System.Collections;

public class DexterityStatusEffect : AbstractStatusEffect
{
    public DexterityStatusEffect()
    {
        this.Name = "Dexterity";
        this.Stackable = true;
        this.AllowedToGoNegative = true;
    }

    public override string Description => $"Whenever you apply defense, apply [Stacks] more.";

    public override int DefenseReceivedAddition()
    {
        return Stacks;
    }
}
