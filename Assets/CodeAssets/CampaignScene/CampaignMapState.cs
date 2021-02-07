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

    public static int Money => ServiceLocator.GetGameStateTracker().money;

    public static ShopData shopData;

    public static List<AbstractAugmentation> AugmentationsInventory { get; set; }
    public static List<AbstractCard> CardsInventory { get; set; }

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

/// <summary>
///  This represents an item that can be assigned from the Equipment Assignment screen.
/// </summary>
public abstract class AbstractAugmentation
{
    public string Title { get; set; }
    public string Description { get; set; }
    public ProtoGameSprite ProtoSprite { get; set; } = GameIconProtoSprite.Default;

    public AbstractBattleUnit Owner { get; set; }

    public Rarity Rarity { get; set; }

    public abstract void OnAssignment(AbstractBattleUnit battleUnit);



    public static AbstractAugmentation GrantsPerkAugmentation(string name,
        string description,
        SoldierPerk perk,
        int stacks = 1,
        Rarity rarity = Rarity.COMMON)
    {
        var aug = new PerkAugmentation(perk, stacks);
        aug.Title = name;
        aug.Description = description;
        aug.Rarity = rarity;
        return aug;
    }

    public static AbstractAugmentation GrantsStatusEffectAugmentation(string name,
        string description,
        AbstractStatusEffect effect, 
        int stacks,
        Rarity rarity)
    {
        var aug = new PerkAugmentation(new GrantsStatusEffectPerk(name, description, effect, stacks));
        aug.Rarity = rarity;
        return aug;
    }
}

public class PerkAugmentation : AbstractAugmentation
{
    SoldierPerk Perk;
    public PerkAugmentation(SoldierPerk perk,
        int stacks = 1
        )
    {
        this.Perk = perk;
        this.Description = perk.Description();
        this.Title = perk.Name();
        perk.Stacks = stacks;
    }

    public override void OnAssignment(AbstractBattleUnit battleUnit)
    {
        battleUnit.ApplySoldierPerk(Perk);
    }
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
    public AbstractAugmentation Augmentation { get; set; }

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
        ServiceLocator.GetGameStateTracker().money -= Price;
        MoneyIcon.Instance.Flash();
        InnerOnPurchase();
    }

    public bool CanAfford()
    {
        return Price <= CampaignMapState.Money;
    }
}

