﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CampaignMapState 
{
    static CampaignMapState()
    {
        InitializeRoster();
        InitializeSelectableMissions();
    }

    public static List<AbstractBattleUnit> Roster;
    public static List<AbstractBattleUnit> CurrentSelectedParty;

    public static List<Mission> MissionsSelectable;
    public static int Money => ServiceLocator.GetGameStateTracker().money;

    public static ShopData shopData;
    
    public static void InitializeRoster()
    {
        Roster = new List<AbstractBattleUnit>
        {
            Rookie.Generate(),
            Rookie.Generate(),
            Rookie.Generate(),
            Rookie.Generate()
        };
    }

    private static MissionGenerator missionGenerator = new MissionGenerator();

    public static void InitializeSelectableMissions() 
    {
        MissionsSelectable = new List<Mission>
        {
            missionGenerator.GenerateNewMission(),
            missionGenerator.GenerateNewMission(),
            missionGenerator.GenerateNewMission()
        };
    }


}

public class ShopData 
{
    public List<AbstractShopOffer> CardsOnOffer;
    public List<AbstractShopOffer> OtherStuffOnOffer;
}


public abstract class AbstractShopOffer
{
    public string Description;
    public string ImagePath;
    public int Price;

    protected abstract void InnerOnPurchase();

    public void Purchase()
    {
        ServiceLocator.GetGameStateTracker().money -= Price;
        MoneyIcon.Instance.Flash();
    }

    public bool CanAfford()
    {
        return Price <= CampaignMapState.Money;
    }
}