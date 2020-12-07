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

    public override List<AbstractCard> UniqueCardRewardPool()
    {
        return new List<AbstractCard>
        {
            new Recklessness(),
            new DeathOrGlory(),
            new Doubletap(),
            new Recklessness(),
            new RunAndGun(),
            new Strafe()
        };
    }
    public override string Name()
    {
        return "Vanguard";
    }
}
