using UnityEngine;
using System.Collections;

public class Grenade : AbstractCard
{
    public Grenade()
    {
        this.Damage = 5;
        TargetType = TargetType.ENEMY;
        Name = "Grenade";
    }

    public override string Description()
    {
        return "Deals 5 damage to all enemies";
    }
}
