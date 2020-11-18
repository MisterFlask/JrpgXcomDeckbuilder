using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RookieClass : AbstractSoldierClass
{
    protected override List<AbstractCard> UniqueCardRewardPool()
    {
        return new List<AbstractCard>()
        {
            new Defend(),
            new Bayonet(),
            new CoveringFire()
        };
    }

    public override List<AbstractCard> StartingCards()
    {
        return new List<AbstractCard>
        {
            new Defend(),
            new Bayonet(),
            new Bayonet(),
            new Defend(),
            new CoveringFire()
        };
    }

    public override void LevelUpAdditionalEffects()
    {

    }

    public override voi

    private List<AbstractSoldierClass> GetClassesEligibleForPromotion()
    {
        return new List<AbstractSoldierClass>
        {
            new VanguardSoldierClass()
        };// todo
    }
}
