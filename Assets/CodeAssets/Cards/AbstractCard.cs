using HyperCard;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractCard
{
    public Guid OwnerGuid;

    public AbstractBattleUnit Owner => ServiceLocator.GetGameStateTracker().PersistentCharacterRoster.First(item => item.Guid == OwnerGuid.ToString());

    public string Name { get; set; } = "Name";

    public Rarity Rarity { get; set; } = Rarity.COMMON;

    public int UpgradeQuantity { get; set; } = 0;

    public TargetType TargetType { get; set; } = TargetType.NO_TARGET_OR_SELF;

    public CardType CardType { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid();

    public int Damage { get; set; } = 0;



    public int CurrentToughness { get; set; } = 0;

    #region convenience functions
    public ActionManager action()
    {
        return ServiceLocator.GetActionManager();
    }

    public List<AbstractBattleUnit> enemies()
    {
        return ServiceLocator.GetGameStateTracker().EnemyUnitsInBattle;
    }
    public List<AbstractBattleUnit> allies()
    {
        return ServiceLocator.GetGameStateTracker().PlayerCharactersInBattle;
    }
    #endregion

    public AbstractCard(CardType cardType = null)
    {
        this.CardType = cardType ?? CardType.TechCard;
    }

    public virtual int EnergyCost()
    {
        return 1;
    }

    public virtual string Description()
    {
        return "";
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

    protected virtual void OnPlay(AbstractBattleUnit target)
    {

    }

    public virtual void InHandAtEndOfTurnAction()
    {

    }

    public void PlayCard(AbstractBattleUnit target)
    {
        OnPlay(target);
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

    }

}

public enum Rarity
{
    COMMON,UNCOMMON,RARE
}

public enum TargetType
{
    NO_TARGET_OR_SELF,
    ENEMY,
    ALLY
}