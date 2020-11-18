using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardSelectScreen : MonoBehaviour
{
    public static CardSelectScreen Instance;
    public CardSelectScreen()
    {
        Instance = this;
    }


    public GridLayoutGroup CardGrid;
    public CardSelectionOption CardTemplate;

    private List<GameObject> CardsDisplayed = new List<GameObject>();

    public static void Hide()
    {
        Instance.gameObject.SetActive(false);
    }

    public void Populate(List<AbstractCard> cardsToDisplay)
    {
        Instance.gameObject.SetActive(true);
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
            // CardTemplate.CardSelectionScreen = this.GetComponent<Popup>();
            var display = cardClone.GetComponent<GameCardDisplay>();
            var hypercard = display.GameCard;
            hypercard.SetToAbstractCardAttributes(card);
            display.transform.SetParent(CardGrid.gameObject.transform);
            display.transform.localScale = Vector3.one;
            CardsDisplayed.Add(cardClone.gameObject);
        }

    }
}
