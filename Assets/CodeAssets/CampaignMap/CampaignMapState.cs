using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CampaignMapState 
{
    public static List<AbstractBattleUnit> Roster;
    public static List<AbstractBattleUnit> CurrentSelectedParty;

    public static List<Mission> MissionsSelectable;
    public static int Money => ServiceLocator.GetGameStateTracker().coins;

    public static ShopData shopData;
    

    public static void InitializeRoster()
    {

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
        ServiceLocator.GetGameStateTracker().coins -= Price;
        MoneyIconGlow.Instance.Flash();
    }

    public bool CanAfford()
    {
        return Price <= CampaignMapState.Money;
    }
}
