using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShowDeckScreen : MonoBehaviour
{
    public ShowDeckScreen()
    {
        Instance = this;
    }

    public GridLayoutGroup CardParent;
    public GameCardDisplay CardTemplate;


    private List<GameObject> CardsDisplayed = new List<GameObject>();

    private static ShowDeckScreen Instance;
    public void Start()
    {
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

    public void HideShowDeckScreen()
    {
        Hide();
    }

    public void Populate(IEnumerable<AbstractCard> cardsToDisplay)
    {
        CardPresentationUtil.PopulateCards(cardsToDisplay, CardsDisplayed, CardTemplate.gameObject, CardParent.gameObject);
    }
}
