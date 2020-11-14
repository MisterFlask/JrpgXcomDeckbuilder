using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RookieClass : AbstractSoldierClass
{
    public override List<AbstractCard> UniqueCardRewardPool()
    {
        throw new System.NotImplementedException();
    }

    public override List<AbstractCard> StartingCards()
    {
        throw new System.NotImplementedException();
    }

    public virtual void LevelUp(AbstractBattleUnit soldier)
    {
        GetNewCardReward();
    }
}
