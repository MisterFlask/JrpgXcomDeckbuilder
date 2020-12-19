using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class CampaignMapState 
{
    static CampaignMapState()
    {
    }

    public static bool GameInitialized { get; set; } = false;

    public static List<AbstractBattleUnit> Roster;
    public static List<AbstractBattleUnit> CurrentSelectedParty;

    public static List<Mission> MissionsActive= new List<Mission>();

    private static string CampaignLog = "";
    public static void AppendLogMessage(string msg)
    {
        CampaignLog += Environment.NewLine + msg;
    }

    public static string GetLogs()
    {
        return CampaignLog;
    }

    public static int Money => ServiceLocator.GetGameStateTracker().money;

    public static ShopData shopData;
    

    public static void InitializeCampaignScreen()
    {
        MissionListPrefab.Instance.Initialize();
        RosterPrefab.Instance.Initialize();
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
