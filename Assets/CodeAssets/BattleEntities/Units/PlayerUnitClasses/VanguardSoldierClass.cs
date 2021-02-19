using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VanguardSoldierClass : AbstractSoldierClass
{
    public override List<AbstractCard> StartingCards()
    {
        return new List<AbstractCard> {

            new Attack(),
            new Attack(),
            new Defend(),
            new Defend(),
        };
    }

    public override string Name()
    {
        return "Vanguard";
    }
}
