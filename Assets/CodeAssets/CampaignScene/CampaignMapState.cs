using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

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

    public static int Money => ServiceLocator.GetGameStateTracker().Credits;

    public static ShopData shopData;

    public static List<SoldierPerk> AugmentationsInventory { get; set; }
    public static List<AbstractCard> CardsInventory { get; set; }

    public static void InitializeCampaignScreen()
    {
        MissionListPrefab.Instance.Initialize();
        RosterPrefab.Instance.Initialize();
    }
}

public class ShopData
{
    public List<ShopCardOffer> CardOffers { get; set; }
    public List<ShopAugmentationOffer> AugmentationOffers { get; set; }

}



public class ShopCardOffer: AbstractShopOffer
{
    public AbstractCard Card { get; set; }

    protected override void InnerOnPurchase()
    {
        CampaignMapState.CardsInventory.Add(Card);
    }
}


public class ShopAugmentationOffer: AbstractShopOffer
{
    public SoldierPerk Augmentation { get; set; }

    protected override void InnerOnPurchase()
    {
        CampaignMapState.AugmentationsInventory.Add(Augmentation);

    }
}

public abstract class AbstractShopOffer
{
    public int Price { get; set; }
    public bool Purchased { get; set; }

    protected abstract void InnerOnPurchase();

    public void Purchase()
    {
        if (Purchased)
        {
            return;
        }
        Purchased = true;
        ServiceLocator.GetGameStateTracker().Credits -= Price;
        MoneyIcon.Instance.Flash();
        InnerOnPurchase();
    }

    public bool CanAfford()
    {
        return Price <= CampaignMapState.Money;
    }
}

