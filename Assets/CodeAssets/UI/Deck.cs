using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

[RequireComponent(typeof(CardAnimationManager))]
// Rules:  Deck should look like face-down card
public class BattleDeck
{

   

    public IEnumerable<AbstractCard> PersistentDeckList => CollectionUtils.Aggregate(DrawPile, DiscardPile, Hand);

    public List<AbstractCard> PurgedPile { get; set; } = new List<AbstractCard>();
    public List<AbstractCard> DrawPile { get; set; } = new List<AbstractCard>();

    public List<AbstractCard> DiscardPile { get; set; } = new List<AbstractCard>();

    public List<AbstractCard> Hand { get; set; } = new List<AbstractCard>();


    public CardPosition GetCardPosition(string cardId)
    {
        if (Hand.Where(item => item.Id == cardId).Any())
        {
            return CardPosition.HAND;
        }
        if (DrawPile.Where(item => item.Id == cardId).Any())
        {
            return CardPosition.DRAW;
        }
        if (DiscardPile.Where(item => item.Id == cardId).Any())
        {
            return CardPosition.DISCARD;
        }
        if (PurgedPile.Where(item => item.Id == cardId).Any())
        {
            return CardPosition.EXPENDED;
        }
        Debug.Log($"Could not find card {cardId}; returning Purged");
        return CardPosition.EXPENDED;
    }

    public CardPosition PurgeCardFromDeck(string id)
    {
        if (!PersistentDeckList.Where(i => i.Id == id).Any())
        {
            throw new Exception("Could not find card with ID of " + id);
        }
        var card = PersistentDeckList.Where(i => i.Id == id).Single();
        if (DrawPile.Contains(card))
        {
            DrawPile.Remove(card);
            return CardPosition.DRAW;

        }
        if (DiscardPile.Contains(card))
        {
            DiscardPile.Remove(card); 
            return CardPosition.DISCARD;
        }
        if (Hand.Contains(card))
        {
            Hand.Remove(card);
            return CardPosition.HAND;
        }
        throw new Exception("This should be impossible");
    }

    public AbstractCard GetRandomMatchingCard(Func<AbstractCard, bool> pred)
    {
        var matchesPred = this.PersistentDeckList.Where(pred);
        if (matchesPred.IsEmpty())
        {
            return null;
        }
        return matchesPred.PickRandom();
    }

    public void DiscardCard(AbstractCard card)
    {
        Hand.Remove(card);
        DiscardPile.Add(card);
    }

    private List<AbstractCard> GetPileForPosition(CardPosition position)
    {
        if (position == CardPosition.DISCARD)
        {
            return DiscardPile;
        }
        if (position == CardPosition.DRAW)
        {
            return DrawPile;
        }
        if (position == CardPosition.HAND)
        {
            return Hand;
        }
        if (position == CardPosition.EXPENDED)
        {
            return PurgedPile;
        }
        throw new Exception($"Don't know about position {position}");
    }

    public void MoveCardToPile(AbstractCard card, CardPosition position, bool newCardsAllowed = false)
    {
        var fromPosition = GetCardPosition(card.Id);
        var fromPile = GetPileForPosition(fromPosition);
        if (!newCardsAllowed && !PersistentDeckList.Contains(card))
        {
            throw new Exception("Card did not exist prior to pile move");
        }
        var toPile = GetPileForPosition(position);
        if (fromPile == null)
        {
            throw new Exception("Could not find card with name " + card.Name);
        }
        fromPile.RemoveAll((item) => item.Id == card.Id);
        toPile.Add(card);
    }

    public void AddNewCardToDeck(AbstractCard card)
    {
        if (PersistentDeckList.Any(item => item.Id == card.Id))
        {
            throw new Exception("Attempted to add duplicate card to deck: " + card.Name);
        }
        DiscardPile.Add(card);
    }

    public void ReshuffleDeck()
    {
        DrawPile.AddRange(DiscardPile);
        DiscardPile.Clear();
        DrawPile = DrawPile.Shuffle().ToList();
    }

    public List<AbstractCard> DrawNextNCards(int n)
    {
        var cardsSoFar = new List<AbstractCard>();
        int cardsToStart = PersistentDeckList.Count();
        try
        {

            for (int i = 0; i < n; i++)
            {
                if (DrawPile.IsEmpty())
                {
                    ReshuffleDeck();
                    if (DrawPile.IsEmpty())
                    {
                        Debug.Log("Deck is empty, discard is empty, yet still we attempt to draw");
                        this.Hand.AddRange(cardsSoFar);

                        return cardsSoFar;
                    }
                }
                cardsSoFar.Add(DrawPile.PopFirstElement());
            }

            this.Hand.AddRange(cardsSoFar);
        }
        finally
        {
            var cardsAfterShuffle = PersistentDeckList.Count();

            if (cardsToStart != cardsAfterShuffle)
            {
                throw new Exception("Validation failure: after shuffle had " + cardsAfterShuffle + " and cards to start were " + cardsToStart);
            }
        }
        return cardsSoFar;
    }

}
public enum CardPosition
{
    HAND,
    DRAW,
    DISCARD,
    EXPENDED
}