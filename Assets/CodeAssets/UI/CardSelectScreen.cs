using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Ricimi;

public class CardSelectScreen : MonoBehaviour
{

    public GridLayoutGroup CardGrid;
    public CardSelectionOption CardTemplate;

    private List<GameObject> CardsDisplayed = new List<GameObject>();

    public void Populate(List<AbstractCard> cardsToDisplay)
    {
        foreach (var card in CardsDisplayed)
        {
            card.transform.parent = null;
            card.Despawn();
        }

        CardsDisplayed.Clear();
        foreach (var card in cardsToDisplay)
        {
            var cardClone = CardTemplate.gameObject.Spawn(CardGrid.transform);
            cardClone.gameObject.SetActive(true);
            CardTemplate.Initialize(card);
            CardTemplate.CardSelectionScreen = this.GetComponent<Popup>();
            var display = cardClone.GetComponent<GameCardDisplay>();
            var hypercard = display.GameCard;
            hypercard.SetToAbstractCardAttributes(card);
            display.transform.SetParent(CardGrid.gameObject.transform);
            display.transform.localScale = Vector3.one;
            CardsDisplayed.Add(cardClone.gameObject);
        }

    }
}
