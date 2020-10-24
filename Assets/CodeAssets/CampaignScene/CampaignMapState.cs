using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CampaignMapState 
{
    static CampaignMapState()
    {
    }

    public static bool Initialized { get; set; } = false;

    public static List<AbstractBattleUnit> Roster;
    public static List<AbstractBattleUnit> CurrentSelectedParty;

    public static List<Mission> MissionsSelectable;
    public static int Money => ServiceLocator.GetGameStateTracker().money;

    public static ShopData shopData;
    

    public static MissionGenerator MissionGenerator = new MissionGenerator();
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
