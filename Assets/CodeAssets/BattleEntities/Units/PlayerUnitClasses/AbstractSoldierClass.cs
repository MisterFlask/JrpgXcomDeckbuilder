﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public abstract class AbstractSoldierClass
{
    public int CurrentLevel = 1;
        
    public int StartingMaxHp = 15;

    /// <summary>
    /// Starting cards are held by ALL members of a class
    /// </summary>
    public abstract List<AbstractCard> StartingCards(); 

    public abstract List<AbstractCard> UniqueCardRewardPool();

    public abstract string Name();

    public List<AbstractCard> GetCardRewardsForLevel(int level, int numRewards)
    {
        var ratios = CardRarityRatio.GetRatioForLevel(level);
        var commons = UniqueCardRewardPool().Where(item => item.Rarity == Rarity.COMMON).Multiply(ratios.CommonsMultiplier);
        var uncommons = UniqueCardRewardPool().Where(item => item.Rarity == Rarity.UNCOMMON).Multiply(ratios.UncommonsMultiplier);
        var rares = UniqueCardRewardPool().Where(item => item.Rarity == Rarity.RARE).Multiply(ratios.RaresMultiplier);

        var takenCards = commons.Union(uncommons).Union(rares)
            .Shuffle().ToList()
            .TakeUnique(numRewards);
        return takenCards;
    }

    public List<AbstractCard> GetCardRewardsOfSpecificRarity(Rarity rarity, int numRewards)
    {
        var takenCards = UniqueCardRewardPool()
            .Where(item => item.Rarity == rarity)
            .Shuffle().ToList()
            .TakeUnique(numRewards);
        return takenCards;
    }


    public virtual List<AbstractCard> GetCardRewardChoices()
    {
        if (UniqueCardRewardPool().IsEmpty())
        {
            throw new System.Exception("Must init card reward pool");
        }

        return UniqueCardRewardPool().Shuffle().Take(3).ToList();
    }

    public void LevelUp(AbstractBattleUnit me)
    {
        CurrentLevel++;
        LevelUpEffects(me);
    }


    public virtual void LevelUpEffects(AbstractBattleUnit me)
    {
        me.NumberCardRewardsEligibleFor++;
        StartingMaxHp += 1;
    }
}


public class CardRarityRatio
{
    public int CommonsMultiplier { get; set; }
    public int UncommonsMultiplier { get; set; }
    public int RaresMultiplier { get; set; }
    public int CharacterLevelRelevant { get; set; }

    public static CardRarityRatio GetRatioForLevel(int characterLevel)
    {
        var levelToRetrieve = Math.Min(characterLevel, 5);
        var appropriateRatio = RatiosByLevel.FirstOrDefault(item => item.CharacterLevelRelevant == levelToRetrieve);

        if (appropriateRatio == null)
        {
            throw new Exception("Couldn't find appropriate card rarity ratio for level " + characterLevel);
        }
        return appropriateRatio;
    }

    private static List<CardRarityRatio> RatiosByLevel = new List<CardRarityRatio>()
    {
        new CardRarityRatio
        {
            CharacterLevelRelevant = 1,
            CommonsMultiplier = 5,
            UncommonsMultiplier = 3,
            RaresMultiplier = 1
        },
        new CardRarityRatio
        {
            CharacterLevelRelevant = 2,
            CommonsMultiplier = 5,
            UncommonsMultiplier = 3,
            RaresMultiplier = 1
        },
        new CardRarityRatio
        {
            CharacterLevelRelevant = 3,
            CommonsMultiplier = 5,
            UncommonsMultiplier = 3,
            RaresMultiplier = 1
        },
        new CardRarityRatio
        {
            CharacterLevelRelevant = 4,
            CommonsMultiplier = 5,
            UncommonsMultiplier = 3,
            RaresMultiplier = 1
        },
        new CardRarityRatio
        {
            CharacterLevelRelevant = 5,
            CommonsMultiplier = 5,
            UncommonsMultiplier = 3,
            RaresMultiplier = 1
        }
    };
}