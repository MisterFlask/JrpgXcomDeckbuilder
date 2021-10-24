using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using Assets.CodeAssets.UI.Subscreens;
using Assets.CodeAssets.CampaignScene.Shop;

public static class CampaignMapState 
{
    static CampaignMapState()
    {
    }

    public static bool GameInitialized { get; set; } = false;

    public static List<AbstractBattleUnit> Roster = new List<AbstractBattleUnit>();
    public static List<AbstractBattleUnit> CurrentSelectedParty = new List<AbstractBattleUnit>();

    public static List<AbstractMission> MissionsActive= new List<AbstractMission>();

    private static string CampaignLog = "";
    public static void AppendLogMessage(string msg)
    {
        CampaignLog += Environment.NewLine + msg;
    }

    public static string GetLogs()
    {
        return CampaignLog;
    }

    public static int Money => ServiceLocator.GameState().Credits;

    public static ShopData shopData;

    public static List<AbstractSoldierPerk> AugmentationsInventory { get; set; }
    public static List<AbstractCard> CardsInventory { get; set; }

    public static void InitializeCampaignScreen()
    {
        MissionListPrefab.Instance.Initialize();
        RosterPrefab.Instance.Initialize();

        ShowDeckScreen.Hide();
        CardRewardScreen.Hide();
        AssignFreeAugmentationsPanel.Instance.Hide();
        SelectCardToAddFromInventoryScreen.Hide();
        ShopScreen.Instance.Hide();
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
    public AbstractSoldierPerk Augmentation { get; set; }

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
        ServiceLocator.GameState().Credits -= Price;
        MoneyIcon.Instance.Flash();
        InnerOnPurchase();
    }

    public bool CanAfford()
    {
        return Price <= CampaignMapState.Money;
    }
}

