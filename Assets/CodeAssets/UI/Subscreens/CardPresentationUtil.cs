using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CardPresentationUtil
{

    public static List<GameCardDisplay> PopulateCards(IEnumerable<AbstractCard> cardsToDisplay, 
        List<GameObject> cardObjectsToDestroy, GameObject cardTemplate, GameObject cardParent)
    {
        foreach (var card in cardObjectsToDestroy)
        {
            card.transform.SetParent(null);
            card.Despawn();
        }
        cardObjectsToDestroy.Clear();

        var gameCardDisplays = new List<GameCardDisplay>();

        foreach (var card in cardsToDisplay)
        {
            var cardClone = cardTemplate.gameObject.Spawn(cardParent.transform);
            cardClone.gameObject.SetActive(true);
            var display = cardClone.GetComponent<GameCardDisplay>();
            var hypercard = display.GameCard;
            hypercard.SetToAbstractCardAttributes(card);

            cardObjectsToDestroy.Add(cardClone.gameObject);
            cardParent.AddChildren(new List<GameObject> { cardClone.gameObject });
            cardClone.localScale = Vector2.one; // only because ui animation seems to shrink it to 0
            gameCardDisplays.Add(display);
        }
        return gameCardDisplays;
    }
}
