using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VanguardSoldierClass : AbstractSoldierClass
{
    public override List<AbstractCard> StartingCards()
    {
        return new List<AbstractCard> {

            new FireSMG(),
            new FireSMG(),
            new Defend(),
            new Defend()
        };
    }

    protected override List<AbstractCard> UniqueCardRewardPool()
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
}
