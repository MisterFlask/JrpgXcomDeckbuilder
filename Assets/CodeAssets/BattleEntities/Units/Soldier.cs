using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soldier : AbstractAllyUnit
{
    public Soldier()
    {
        this.MaxHp = 10;
        this.MaxFatigue = 4;
        this.SoldierClass = new RookieClass();

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
        return new Soldier().CloneUnit();
    }

    public override List<AbstractCard> CardsSelectableOnLevelUp()
    {
        throw new System.NotImplementedException();
    }
}
