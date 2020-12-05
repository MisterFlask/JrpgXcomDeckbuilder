
using HyperCard;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractCard
{
    public Guid OwnerGuid;

    public int FatigueCost => 1;
    public AbstractBattleUnit Owner { get; set; }

    public string Name { get; set; } = "Name";

    public Rarity Rarity { get; set; } = Rarity.COMMON;

    public int UpgradeQuantity { get; set; } = 0;

    public TargetType TargetType { get; set; } = TargetType.NO_TARGET_OR_SELF;

    public CardType CardType { get; set; }

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public int BaseDamage { get; set; } = 0;

    public int BaseDefenseValue { get; set; } = 0;

    /// <summary>
    /// Similar to focus; non-general-purpose and not applicable to all cards
    /// </summary>
    public int BaseTechValue { get; set; } = 0;

    public int TechValue => BaseTechValue;

    public int CurrentToughness { get; set; } = 0;

    public List<AbstractCardSticker> Stickers = new List<AbstractCardSticker>();

    #region convenience functions

    public int displayedDamage()
    {
        return BattleRules.GetDisplayedDamageOnCard(this);
    }

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
        return ServiceLocator.GetGameStateTracker().AllyUnitsInBattle;
    }

    public GameState state()
    {
        return ServiceLocator.GetGameStateTracker();
    }
    #endregion

    public AbstractCard(CardType cardType = null)
    {
        this.CardType = cardType ?? CardType.SkillCard;
        this.Name = this.GetType().Name;
    }
    int? StaticBaseEnergyCost = null;
    public virtual int BaseEnergyCost()
    {
        return StaticBaseEnergyCost ?? 1;
    }

    public int EnergyCostMod = 0;

    public int EnergyCost => EnergyCostMod + BaseEnergyCost();

    public abstract string Description();


    public virtual bool CanAfford()
    {
        return ServiceLocator.GetGameStateTracker().energy >= EnergyCost;
    }

    public virtual bool CanPlay()
    {
        if (Unplayable)
        {
            return false;
        }

        return CanAfford();
    }

    public virtual void OnDraw()
    {
        
    }

    protected abstract void OnPlay(AbstractBattleUnit target);

    public virtual void InHandAtEndOfTurnAction()
    {

    }

    public int DisplayedDefense()
    {
        return BattleRules.GetDisplayedDefenseOnCard(this);
    }
    public int DisplayedDamage()
    {
        return BattleRules.GetDisplayedDamageOnCard(this);
    }

    public void PlayCardFromHandIfAble(AbstractBattleUnit target)
    {
        if (!CanPlay())
        {
            return;
        }

        BattleRules.ProcessPlayingCardCost(this);
        OnPlay(target);
        
        if (state().Deck.Hand.Contains(this))
        {
            state().Deck.MoveCardToPile(this, CardPosition.DISCARD);
            ServiceLocator.GetCardAnimationManager().MoveCardToDiscardPile(this, assumedToExistInHand: true);
        }
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


    public ProductionAction GetApplicableProductionAction()
    {
        return ProductionAction.UPGRADE_CARD;
    }


    public void Upgrade()
    {
        this.UpgradeQuantity += 1;
    }

    /// <summary>
    /// logicallyIdenticalToExistingCard just means that the card is keeping its ID, since it's logically the same card as in the character's persistent decks.
    /// This distinction here matters because things that happen to cards in combat don't in general affect the character's persistent deck.
    /// It's pretty much ONLY true when we're first initializing the battle deck in a combat.
    /// </summary>
    public AbstractCard CopyCard(bool logicallyIdenticalToExistingCard = false)
    {
        var copy = (AbstractCard)this.MemberwiseClone();
        copy.Stickers = new List<AbstractCardSticker>();
        foreach (var sticker in copy.Stickers)
        {
            var newSticker = sticker.CopySticker();
            copy.Stickers.Add(newSticker);
        }

        CopyCardInner(copy);
        if (!logicallyIdenticalToExistingCard)
        {
            copy.Id = Guid.NewGuid().ToString();
        }
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

    public void AddSticker(AbstractCardSticker sticker)
    {
        Stickers.Add(sticker);
    }

    public  void RemoveSticker<T>() where T: AbstractCardSticker
    {
        var sticker = Stickers.FirstOrDefault(item => item.GetType() == typeof(T));
        if (sticker == null)
        {
            return;
        }
        Stickers.Remove(sticker);
    }

    public bool Unplayable { get; set; }

    public virtual void OnTookDamageWhileInHand()
    {

    }

    public string OwnerDisplayName()
    {
        return Owner.CharacterName;
    }

    public void SetCommonCardAttributes(string name,
        Rarity rarity,
        TargetType targetType,
        CardType cardType,
        int baseEnergyCost)
    {
        this.Name = name;
        this.Rarity = rarity;
        this.TargetType = targetType;
        this.CardType = cardType;
        this.StaticBaseEnergyCost = baseEnergyCost;
    }


}

public class DamageBlob
{
    public int Damage { get; set; }
    public bool IsAttackDamage { get; set; }
    public bool IsDamagePreview { get; set; } = false;
}


public enum Rarity
{
    COMMON,UNCOMMON,RARE,BASIC,NOT_IN_CARD_POOL
}

public class TargetType
{
    public static TargetType NO_TARGET_OR_SELF = new TargetType();
    public static TargetType ENEMY = new TargetType();
    public static TargetType ALLY = new TargetType();
}