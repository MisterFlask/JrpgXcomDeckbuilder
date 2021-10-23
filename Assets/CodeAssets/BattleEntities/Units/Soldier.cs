using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Soldier : AbstractAllyUnit
{
    public static Soldier NULL_SOLDIER = new Soldier();

    public Soldier(AbstractSoldierClass soldierClass = null)
    {
        this.MaxHp = 10;
        this.MaxFatigue = 4;
        this.SoldierClass = soldierClass ?? new RookieClass();

        this.StartingCardsInDeck.AddRange(SoldierClass.StartingCards());
        this.ProtoSprite = GetRandomProtoSprite();
    }

    private ProtoGameSprite GetRandomProtoSprite()
    {
        return OryxSprites.SelectRandomCharacterSpriteWithRandomColoration();
    }

    public static AbstractBattleUnit GenerateFreshRookie()
    {
        var rookie= new Soldier().CloneUnit();

        return rookie;
    }

    public static AbstractBattleUnit GenerateSoldierOfClass(AbstractSoldierClass soldierClass,
        int level = 1)
    {
        var soldier= new Soldier(soldierClass).CloneUnit();
        for(int i = 1; i < level; i++)
        {
            soldier.LevelUp();
        }
        return soldier;
    }

    public override List<AbstractCard> CardsSelectableOnLevelUp()
    {
        throw new System.NotImplementedException();
    }
}
