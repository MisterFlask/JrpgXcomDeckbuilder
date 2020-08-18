using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using HyperCard;

public class ShowDeckScreen : MonoBehaviour
{

    public GridLayoutGroup CardGrid;
    public GameCardDisplay CardTemplate;
    private List<GameObject> CardsDisplayed = new List<GameObject>();
    public void Populate(List<AbstractCard> cardsToDisplay)
    {
        foreach(var card in CardsDisplayed)
        {
            card.transform.parent = null;
            card.Despawn();
        }
        CardsDisplayed.Clear();

        foreach (var card in cardsToDisplay)
        {
            var cardClone = ServiceLocator.GetSpawnPool().Spawn("CARD_HOLDER_TEMPLATE");
            cardClone.gameObject.SetActive(true);
            var display = cardClone.GetComponent<GameCardDisplay>();
            var hypercard = display.GameCard; 
            hypercard.SetToAbstractCardAttributes(card);

            CardsDisplayed.Add(cardClone.gameObject);
            CardGrid.AddChildren(new List<GameObject> { cardClone.gameObject });
            cardClone.localScale = Vector2.one; // only because ui animation seems to shrink it to 0
        }

    }
}
