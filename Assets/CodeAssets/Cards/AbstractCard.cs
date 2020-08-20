using HyperCard;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractCard
{
    public string Name { get; set; } = "Name";

    public Rarity Rarity { get; set; } = Rarity.COMMON;

    public int UpgradeQuantity { get; set; } = 0;

    public List<CardTag> Tags { get; } = new List<CardTag>();


    public CardType CardType { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid();

    public int BasePower { get; set; } = 0;

    public virtual int Power => BasePower;

    public int MaxToughness { get; set; } = 0; // This being >1 means the card's a Legion.

    public int CurrentToughness { get; set; } = 0;

    public ActionManager action()
    {
        return ServiceLocator.GetActionManager();
    }
    public AbstractCard(CardType cardType = null)
    {
        this.CardType = cardType ?? CardType.TechCard;
    }

    // Only applicable to building cards
    public virtual bool EligibleForConstructingOnProvince(TileLocation location)
    {
        return false;
    }

    public virtual int ProductionCostForNextUpgrade()
    {
        return 20 + UpgradeQuantity * 5;
    }

    public virtual void OnDeployToRegion(TileLocation tileLocation)
    {

    }

    public virtual int EnergyCost()
    {
        return 1;
    }

    public virtual string Description()
    {
        return "";
    }

    public bool IsLegion()
    {
        return MaxToughness > 0;
    }


    public virtual IEnumerable<TileLocation> GetTileLocationsThatCanDeployTo()
    {
        return new List<TileLocation>();
    }

    public virtual bool CanDeployToRegion(TileLocation tileLocation)
    {
        return false;
    }

    public virtual bool CanAfford()
    {
        return ServiceLocator.GetGameStateTracker().energy >= this.EnergyCost();
    }

    public virtual bool CanPlay()
    {
        return CanAfford();
    }

    public virtual void OnDraw()
    {
        
    }

    protected virtual void OnPlay()
    {

    }

    public virtual void InHandAtEndOfTurnAction()
    {

    }
    private void PerformDefaultCardPlayEffects()
    {
        PayCostsForCard();
        if (ExhaustedCount > 0)
        {
            ExhaustedCount--;
            return;
        }


    }
    public virtual void PlayCard()
    {
        PerformDefaultCardPlayEffects();
        OnPlay();
    }

    public Card CreateHyperCard()
    {
        var cardInstantiator = ServiceLocator.GetCardInstantiator();
        var card = cardInstantiator.CreateCard();
        card.SetToAbstractCardAttributes(this);
        card.LogicalCard = this;
        card.GetComponent<PlayerCard>().LogicalCard = this;

        return card;
    }

    private string ConvertTypeToString(CardType type)
    {
        return type.ToString();
    }

    public virtual void CopyCardInner(AbstractCard card)
    {

    }

    public void PayCostsForCard()
    {
        ServiceLocator.GetActionManager().ModifyEnergy(-1 * this.EnergyCost(), ModifyType.ADD_VALUE);
    }

    public ProductionAction GetApplicableProductionAction()
    {
        return ProductionAction.UPGRADE_CARD;
    }


    public void Upgrade()
    {
        this.UpgradeQuantity += 1;
    }

    public AbstractCard CopyCard()
    {
        var copy = (AbstractCard)this.MemberwiseClone();
        CopyCardInner(copy);
        copy.Id = Guid.NewGuid();
        return copy;
    }

    public string CreateCardTagDescriptorString()
    {
        var str = string.Join("; ", this.Tags.Select(item => item.ToString()));
        return str;
    }

    #region specific effects
    /// <summary>
    /// Playing a card with >0 of this causes nothing to happen except this goes down by 1.
    /// </summary>
    public int ExhaustedCount { get; set; }

    #endregion

    public Card FindCorrespondingHypercard()
    {
        return ServiceLocator.GetCardAnimationManager().GetGraphicalCard(this);
    }

    public AbstractCard GetTransformedCardOnProductionAction()
    {
        throw new NotImplementedException("This must be overridden if ProductionAction is 'transform card'");
    }

    public void _Initialize()
    {
        // does postprocessing work

        this.CurrentToughness = MaxToughness;
    }

}

public enum Rarity
{
    COMMON,UNCOMMON,RARE
}