using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HyperCard;
using System.Collections.Generic;
using System.Linq;
using System;
using Assets.CodeAssets.UI.Screens.BattleScreen;

public class CardAnimationManager : MonoBehaviour
{
    public static CardAnimationManager INSTANCE;

    public CardAnimationManager()
    {
        INSTANCE = this;
    }

    GameObject handPanel;

    public List<Card> cardsInHand;
    GameObject leftHandMarker;
    GameObject rightHandMarker;
    GameObject selectedHandBottomMarker;
    GameObject defaultHandBottomMarker;
    // Card select area follows
    GameObject cardSelectAreaBottomMarker;
    Canvas canvas;
    RectTransform SelectedCardsHolder => SelectCardInHandOverlay.INSTANCE.SelectedCardHolder;
    Card cardTemplate;

    GameObject cardSpawnPoint;

    public static Vector3 NaturalCardSize { get; internal set; } = new Vector2(1.2f, 1.2f);
    public static Vector2 EnlargedCardSize { get; internal set; } = new Vector2(1.6f, 1.6f);


    public static List<AbstractCard> SelectedCardsInHand => INSTANCE.cardsInHand
        .Where(item => item.IsSelected)
        .Select(item => item.LogicalCard).ToList();

    // Use this for initialization
    void Start()
    {
        INSTANCE = this;

        cardSpawnPoint = GameObject.Find("CARD_SPAWN_POINT");
        cardsInHand = new List<Card>();
        leftHandMarker = GameObject.Find("LEFT_HAND_BORDER");
        rightHandMarker = GameObject.Find("RIGHT_HAND_BORDER");
        defaultHandBottomMarker = GameObject.Find("BOTTOM_OF_HAND_PANEL_DEFAULT");
        selectedHandBottomMarker = GameObject.Find("BOTTOM_OF_HAND_PANEL_SELECTED");
        canvas = GameObject.FindObjectOfType<Canvas>();
        cardTemplate = GameObject.Find("CARD_TEMPLATE").GetComponent<Card>() ;
        ScriptExecutionTracker.ScriptsFinishedExecuting.Add(nameof(CardAnimationManager));
    }


    public void RefreshCardAppearance(AbstractCard card)
    {
        var hypercard = this.cardsInHand.SingleOrDefault(item => item.GetComponent<PlayerCard>().LogicalCard.Id == card.Id);
        if (hypercard == null)
        {
            return;
        }
        hypercard.SetToAbstractCardAttributes(card);
    }

    internal void RemoveHypercardFromHand(AbstractCard card)
    {
        this.cardsInHand.RemoveAll(item => item.LogicalCard.Id == card.Id);
    }

    public Vector3 GetAppropriateScale(bool isMousedOver)
    {
        if (isMousedOver)
        {
            return EnlargedCardSize;
        }
        else
        {
            return NaturalCardSize;
        }
    }

    public Vector3 GetAppropriateCardRotation(int i, int cardsInHand, bool isMousedOver, bool isSelected)
    {
        if (isMousedOver)
        {
            return new Vector3(0, 0, 0);
        }
        if (isSelected)
        {
            return new Vector3(0, 0, 0);
        }

        var startRotationOffsetInDegrees = 20;
        var endRotationOffsetInDegrees = -20;

        var offsetChangePerCard = (endRotationOffsetInDegrees - startRotationOffsetInDegrees) / cardsInHand;
        var offsetForThisCard = offsetChangePerCard * i + startRotationOffsetInDegrees;

        return new Vector3(0, 0, offsetForThisCard);
    }
    public Vector3 GetAppropriateCardPositionInHand(int i, int cardsInHand, bool isMousedOver)
    {
        var box = cardTemplate.GetComponent<BoxCollider2D>();
        var cardWidth = box.bounds.extents[0] ;
        var cardHeight = box.bounds.extents[1] ;

        var left = leftHandMarker.transform.localPosition.x + cardWidth;
        var right = rightHandMarker.transform.localPosition.x + cardWidth;

        var differenceZPerCard = 1f / cardsInHand;
        var z = Constants.CARD_Z + i * differenceZPerCard;
        var y = defaultHandBottomMarker.transform.localPosition.y + cardHeight;
        if (isMousedOver)
        {
            y = selectedHandBottomMarker.transform.localPosition.y + cardHeight;
            z = z - 1; // should make it smallest Z value, which implies highest priority
        }
        var width = right - left;
        var widthPerCard = width / cardsInHand;
        var cardX = i * widthPerCard + left;
        
        return new Vector3(cardX, y, z);
    }

    public void AddHypercardsToHand(IList<Card> cards)
    { 
        // very slow
        Profiler.Profile(nameof(AddHypercardsToHand), () =>
        {
            var idsOfCardsInHand = cardsInHand.Select(item => item.LogicalCard.Id);
            var idsOfCardsToAdd = cards.Select(item => item.LogicalCard.Id);
            foreach(var inHand in idsOfCardsInHand)
            {
                if (idsOfCardsToAdd.Contains(inHand))
                {
                    throw new Exception($"Could not add card {cards.Select(item => item.LogicalCard.Name).AsString()} because it overlaps with one or more cards in hand");
                }
            }

            foreach (var card in cards)
            {
                cardsInHand.Add(card);
            }

            foreach (var item in cardsInHand)
            {
                MoveCardToAppropriateLocation(item, false);
            }
        });
    }

    public void ReorientAllCards()
    {
        foreach (var card in cardsInHand)
        {
            MoveCardToAppropriateLocation(card, false);
        }
    }

    public void MoveCardToAppropriateLocation(Card card, bool mousedOver)
    {
        var index = GetHandIndexOfCard(card);
        var scriptableMovement = card.GetComponent<CardMovementBehaviors>();
        scriptableMovement.KillMovement();

        if (card.IsSelected)
        {
            scriptableMovement.MoveToLocation(GetAppropriateCardPositionInCardSelectionArea(index, cardsInHand.Count));
        }
        else
        {
            scriptableMovement.MoveToLocation(GetAppropriateCardPositionInHand(index, cardsInHand.Count, mousedOver),
                GetAppropriateCardRotation(index, cardsInHand.Count, mousedOver, card.IsSelected), GetAppropriateScale(mousedOver));
        }
        // now, we reorder the cards on the canvas according to z-axis
        var hand = GameObject.Find("Hand");
        hand.ReorderChildren((child) => child.position.z);
    }

    private Vector3 GetAppropriateCardPositionInCardSelectionArea(int i, int cardsInHand)
    {
        var box = cardTemplate.GetComponent<BoxCollider2D>();
        var cardWidth = box.bounds.extents[0];
        var cardHeight = box.bounds.extents[1];

        var left = leftHandMarker.transform.localPosition.x + cardWidth;
        var right = rightHandMarker.transform.localPosition.x + cardWidth;

        var differenceZPerCard = 1f / cardsInHand;
        var z = Constants.CARD_Z + i * differenceZPerCard;
        var y = selectedHandBottomMarker.transform.localPosition.y + cardHeight;
        var width = right - left;
        var widthPerCard = width / cardsInHand;
        var cardX = i * widthPerCard + left;

        return new Vector3(cardX, y, z);
    }

    private int GetHandIndexOfCard(Card card)
    {
        return this.cardsInHand.IndexOf(card);
    }

    public Card GetGraphicalCard(AbstractCard abstractCard)
    {
        var hypercard = this.cardsInHand.SingleOrDefault(item => item.GetComponent<PlayerCard>().LogicalCard.Id == abstractCard.Id);
        return hypercard;
    }

    public CardMovementBehaviors GetCardMovementBehavior(AbstractCard abstractCard)
    {
        var hypercard = this.cardsInHand.Single(item => item.GetComponent<PlayerCard>().LogicalCard.Id == abstractCard.Id);
        return hypercard.GetComponent<CardMovementBehaviors>();
    }

    public void MoveCardToDiscardPile
        (AbstractCard abstractCard, bool assumedToExistInHand)
    {
        try {
            var logicalCardsInHand = this.cardsInHand.Select(item => item.LogicalCard).ToList();
            var hypercard = this.cardsInHand.Single(item => item.LogicalCard.Id == abstractCard.Id);
            if (cardsInHand.Contains(hypercard))
            {
                //hypercard.GetComponent<CardMovementBehaviors>().DissolveAndDestroyCard();
                hypercard.GetComponent<CardMovementBehaviors>().MoveAndShrinkToLocation(cardSpawnPoint.transform.localPosition);
            }
            else
            {
                throw new System.Exception("Attempted to discard card that doesn't exist");
            }
            cardsInHand.Remove(hypercard);
            ReorientAllCards();
        }
        catch (InvalidOperationException e)
        {
            if (assumedToExistInHand)
            {
                Debug.LogError($"Failed to move card {abstractCard.Name} because {e.Message}");
            }
        }
    }

    public void RunCreateNewCardAndAddToDiscardPileAnimation(AbstractCard card)
    {
        StartCoroutine(MoveToCenterOfScreenWaitAndThenMoveToDiscard(card));
    }

    private IEnumerator MoveToCenterOfScreenWaitAndThenMoveToDiscard(AbstractCard card)
    {
        var cardPrefab = card.CreateHyperCard();
        var leashingBehavior = cardPrefab.GetComponent<CardUiBehaviors>();
        leashingBehavior.enabled = false;
        var movementBehavior = cardPrefab.GetComponent<CardMovementBehaviors>();
        var cardSpawnPoint = GameObject.Find("CARD_SPAWN_POINT").transform.localPosition;
        var discardSpot = GameObject.Find("discard_pile").transform.localPosition;
        yield return movementBehavior.MoveToPosition(cardSpawnPoint,
            cardSpawnPoint,
            cardPrefab.transform.localEulerAngles,
            .4f,
            cardPrefab.transform.localScale,
            false,
            null);
        yield return new WaitForSeconds(.5f);
        yield return movementBehavior.MoveToPosition(cardSpawnPoint,
            discardSpot,
            cardPrefab.transform.localEulerAngles,
            .4f,
            cardPrefab.transform.localScale,
            true,
            null);
        leashingBehavior.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            foreach(var item in cardsInHand)
            {
                item.GetComponent<CardMovementBehaviors>().DissolveAndDestroyCard();
            }
            cardsInHand.Clear();
        }

    }
}
