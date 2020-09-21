using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rookie : AbstractAllyUnit
{
    public Rookie()
    {
        this.MaxHp = 10;
        this.MaxFatigue = 4;
        this.UnitClassName = "Rookie";

        this.StartingCardsInDeck.AddRange(new List<AbstractCard>
        {
            new Grenade(),
            new CoveringFire(),
            new CoveringFire(),
            new Bayonet(),
            new Bayonet(),
            new Defend(),
            new Defend(),
            new Defend(),
            new Defend()
        });
    }

    public static AbstractBattleUnit Generate()
    {
        return new Rookie().InitPersistentUnitFromTemplate();
    }

    public override List<AbstractCard> CardsSelectableOnLevelUp()
    {
        throw new System.NotImplementedException();
    }
}
