using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShowDeckScreen : MonoBehaviour
{

    public GridLayoutGroup CardParent;
    public GameCardDisplay CardTemplate;


    private List<GameObject> CardsDisplayed = new List<GameObject>();

    private static ShowDeckScreen Instance;

    public void Start()
    {
        Instance = this;
    }

    public static void ShowDiscardPile()
    {
        Show(GameState.Instance.Deck.DiscardPile);
    }
    public static void ShowDrawPile()
    {
        Show(GameState.Instance.Deck.DrawPile);
    }
    public static void ShowExhaustPile()
    {
        Show(GameState.Instance.Deck.ExhaustPile);
    }

    public static void ShowDeckForSelectedCharacter()
    {
        if (GameState.Instance.CharacterSelected != null)
        {
            Show(cardsToDisplay: GameState.Instance.CharacterSelected.CardsInPersistentDeck);
        }
        else
        {
            throw new System.Exception("No character selected!");
        }
    }

    public static void Show(IEnumerable<AbstractCard> cardsToDisplay)
    {
        Instance.Populate(cardsToDisplay);
        Instance.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        Instance.gameObject.SetActive(false);
    }

    public void Populate(IEnumerable<AbstractCard> cardsToDisplay)
    {
        foreach (var card in CardsDisplayed)
        {
            card.transform.parent = null;
            card.Despawn();
        }
        CardsDisplayed.Clear();

        foreach (var card in cardsToDisplay)
        {
            var cardClone = CardTemplate.gameObject.Spawn(CardParent.transform);
            cardClone.gameObject.SetActive(true);
            var display = cardClone.GetComponent<GameCardDisplay>();
            var hypercard = display.GameCard;
            hypercard.SetToAbstractCardAttributes(card);

            CardsDisplayed.Add(cardClone.gameObject);
            CardParent.AddChildren(new List<GameObject> { cardClone.gameObject });
            cardClone.localScale = Vector2.one; // only because ui animation seems to shrink it to 0
        }

    }
}
