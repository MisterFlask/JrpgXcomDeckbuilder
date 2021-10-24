using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CardPresentationUtil
{
    private static bool IsPrefab(GameObject obj)
    {
        return obj.scene.name == null;
    }

    public static List<GameCardDisplay> PopulateCards(IEnumerable<AbstractCard> cardsToDisplay, 
        List<GameObject> cardObjectsToDestroy, GameObject cardTemplate, GameObject cardParent)
    {

        //validation 

        if (IsPrefab(cardParent))
        {
            throw new Exception("Attempted to parent object to a prefab: " + cardParent.name + ":" + cardParent.GetType().Name);
        }

        //end validation
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

            cardObjectsToDestroy.Add(cardClone.gameObject);
            cardParent.AddChildren(new List<GameObject> { cardClone.gameObject });


            cardClone.gameObject.SetActive(true);
            var display = cardClone.GetComponent<GameCardDisplay>();
            var hypercard = display.GameCard;
            hypercard.SetToAbstractCardAttributes(card);

            cardClone.localScale = Vector2.one; // only because ui animation seems to shrink it to 0
            gameCardDisplays.Add(display);
        }
        return gameCardDisplays;
    }
}
