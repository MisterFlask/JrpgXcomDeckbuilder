﻿
using Assets.CodeAssets.Cards;
using Assets.CodeAssets.GameLogic;
using HyperCard;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractCard
{
    public AbstractCard ReferencesAnotherCard { get; set; }

    /// <summary>
    /// if this is empty, any class can use it.
    /// </summary>
    public List<Type> SoldierClassCardPools { get; } = new List<Type>();
        
    public Guid OwnerGuid;
    // TODO: Move to a more explicit magic-words system
    public List<MagicWord> MagicWordsReferencedOnThisCard { get; set; } = new List<MagicWord>();

    /// <summary>
    ///  This is just a thing tracking the countdown on a card.
    /// </summary>
    public int Countdown { get; set; } = -1;

    /// <summary>
    /// I just added this for debugging purposes
    /// </summary>
    public GameState GameState => GameState.Instance;

    public ProtoGameSprite ProtoSprite = ProtoGameSprite.FromGameIcon();
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

    public List<string> CardTags { get; set; } = new List<string>();

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

    /// <summary>
    /// This represents the energy cost PHYSICALLY ON THE CARD
    /// that means things like stickers can modify this.
    /// </summary>
    /// <returns></returns>
    public virtual int BaseEnergyCost()
    {
        return StaticBaseEnergyCost ?? 1;
    }


    /// <summary>
    /// This represents the energy cost ACTUALLY PAID (e.g. via bloodprice)
    /// </summary>
    /// <returns></returns>
    public virtual EnergyPaidInformation GetNetEnergyCost()
    {
        return new EnergyPaidInformation
        {
            EnergyCost = BaseEnergyCost()
        };
    }

    public int EnergyCostMod = 0;

    public int EnergyCost => EnergyCostMod + BaseEnergyCost();

    public abstract string DescriptionInner();

    public string Description()
    {
        var baseDescription = DescriptionInner();
        foreach(var sticker in Stickers)
        {
            baseDescription += "\n" + sticker.CardDescriptionAddendum;
        }
        return baseDescription;
    }

    public virtual bool CanAfford()
    {
        return ServiceLocator.GetGameStateTracker().energy >= GetNetEnergyCost().EnergyCost;
    }

    public virtual CanPlayCardQueryResult CanPlayInner()
    {
        return CanPlayCardQueryResult.CanPlay();
    } 

    public CanPlayCardQueryResult CanPlay()
    {
        if (Unplayable)
        {
            return CanPlayCardQueryResult.CannotPlay("This card is unplayable.");
        }

        if (!CanPlayInner().Playable)
        {
            return CanPlayInner();
        }

        if (CanAfford())
        {
            return CanPlayCardQueryResult.CanPlay();
        }
        else
        {
            return CanPlayCardQueryResult.CannotPlay("I don't have the energy for this.");
        }
    }

    public virtual void OnDrawInner()
    {
        
    }
    
    public void OnDraw()
    {
        OnDrawInner();
        foreach(var sticker in Stickers)
        {
            sticker.OnCardDrawn(this);
        }
    }

    /// <summary>
    ///  returns -1 if the card's not in hand.
    /// </summary>
    /// <returns></returns>
    public int GetCardPosition()
    {
        var cardsInHand = state().Deck.Hand;
        var index = cardsInHand.IndexOf(this);
        return index;
    }
    
    public virtual bool ShouldRetainCardInHandAtEndOfTurn()
    {
        return false;
    }

    public abstract void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid);

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

    public void PlayCardFromHandIfAble_Action(AbstractBattleUnit target)
    {
        if (!CanPlay().Playable)
        {
            return;
        }
        var costPaid = BattleRules.ProcessPlayingCardCost(this);
        EvokeCardEffect(target, costPaid);
        BattleRules.RunOnPlayCardEffects(this, target, costPaid);
        if (state().Deck.Hand.Contains(this))
        {
            state().Deck.MoveCardToPile(this, CardPosition.DISCARD);
            ServiceLocator.GetCardAnimationManager().MoveCardToDiscardPile(this, assumedToExistInHand: true);
        }
        state().cardsPlayedThisTurn += 1;
    }

    public void EvokeCardEffect(AbstractBattleUnit target, EnergyPaidInformation costPaid = null)
    {
        if (costPaid == null)
        {
            costPaid = new EnergyPaidInformation
            {
                EnergyCost = 0
            };
        }
        OnPlay(target, costPaid);
        foreach(var sticker in Stickers)
        {
            sticker.OnCardPlayed(this, target);
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


    public string OwnerDisplayName()
    {
        return Owner.CharacterFullName;
    }

    public void SetCommonCardAttributes(string name,
        Rarity rarity,
        TargetType targetType,
        CardType cardType,
        int baseEnergyCost,
        ProtoGameSprite protoGameSprite = null)
    {
        this.Name = name;
        this.Rarity = rarity;
        this.TargetType = targetType;
        this.CardType = cardType;
        this.StaticBaseEnergyCost = baseEnergyCost;
        if (protoGameSprite != null)
        {
            this.ProtoSprite = protoGameSprite; 
        }
    }

    public bool IsValidForClass(AbstractBattleUnit unit)
    {
        if (unit == null)
        {
            return false;
        }

        return SoldierClassCardPools.IsEmpty() 
            || SoldierClassCardPools.Contains(unit.SoldierClass.GetType());
    }

    /// <summary>
    /// 
    /// </summary>
    public AbstractCard CorrespondingPermanentCard()
    {
        var persistentDeck = this.Owner.CardsInPersistentDeck;
        var permanentCard = persistentDeck.First(item => item.Id == this.Id);
        return permanentCard;
    }

    public virtual void IsNotExhaustedInDeckAtEndOfBattle()
    {

    }

}

public class DamageBlob
{
    public int Damage { get; set; }
    public bool IsAttackDamage { get; set; }
}


public enum Rarity
{
    COMMON,UNCOMMON,RARE,BASIC,NOT_IN_POOL
}

public class TargetType
{
    public static TargetType NO_TARGET_OR_SELF = new TargetType();
    public static TargetType ENEMY = new TargetType();
    public static TargetType ALLY = new TargetType();
}

public class BattleCardTags
{
    public static string SWARM = "swarm";
    public static string VIGIL = "vigil";
}

public class CanPlayCardQueryResult
{
    public static CanPlayCardQueryResult CannotPlay(string reason)
    {
        return new CanPlayCardQueryResult
        {
            Playable = false,
            ReasonUnplayable = reason
        };
    }

    public static CanPlayCardQueryResult CanPlay()
    {
        return new CanPlayCardQueryResult
        {
            Playable = true
        };
    }

    public string ReasonUnplayable { get; set; }
    public bool Playable { get; set; }
}