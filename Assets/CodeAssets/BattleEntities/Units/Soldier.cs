using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soldier : AbstractAllyUnit
{
    public Soldier(AbstractSoldierClass soldierClass = null)
    {
        this.MaxHp = 10;
        this.MaxFatigue = 4;
        this.SoldierClass = soldierClass ?? new RookieClass();

        this.StartingCardsInDeck.AddRange(SoldierClass.StartingCards());
    }

    public static AbstractBattleUnit GenerateRookie()
    {
        return new Soldier().CloneUnit();
    }

    public static AbstractBattleUnit GenerateSoldier(AbstractSoldierClass soldierClass)
    {
        return new Soldier(soldierClass);
    }

    public override List<AbstractCard> CardsSelectableOnLevelUp()
    {
        throw new System.NotImplementedException();
    }
}
