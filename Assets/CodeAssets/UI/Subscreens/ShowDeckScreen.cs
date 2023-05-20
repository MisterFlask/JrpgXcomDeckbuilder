﻿using Assets.CodeAssets.Utils;
using HyperCard;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShowDeckScreen : EagerMonobehaviour
{
    public ShowDeckScreen()
    {
    }

    public override void AwakenOnSceneStart()
    {
        if (this.IdentifierName != "Default" && this.IdentifierName != "")
        {
            Instance = this;
        }
    }

    public string IdentifierName;
    public GridLayoutGroup CardParent;
    public GameCardDisplay CardTemplate;
    public Button HideShowDeckScreenButton;
    public TMPro.TextMeshProUGUI DisplayText;

    private List<GameObject> CardsDisplayed = new List<GameObject>();

    public static ShowDeckScreen Instance;

    private static int numTimesInitialized = 0;
    public void Start()
    {
        Log.Info("Starting ShowDeckScreen prefab; has been done " + numTimesInitialized + " times before.");
        numTimesInitialized++;

        // validation
        if (HideShowDeckScreenButton == null)
        {
            throw new Exception("Hide/show deck screen button is null; screen: " + IdentifierName);
        }
    }

    public static void ShowDiscardPile()
    {
        ShowDeckScreen.Instance.HideShowDeckScreenButton.gameObject.SetActive(true);
        Show(GameState.Instance.Deck.DiscardPile, "Discard Pile", true);
    }
    public static void ShowDrawPile()
    {
        ShowDeckScreen.Instance.HideShowDeckScreenButton.gameObject.SetActive(true);
        Show(GameState.Instance.Deck.DrawPile, "Draw Pile", true);
    }
    public static void ShowExhaustPile()
    {
        ShowDeckScreen.Instance.HideShowDeckScreenButton.gameObject.SetActive(true);
        Show(GameState.Instance.Deck.ExhaustPile, "Exhaust Pile", true);
    }

    public static void ShowDeckForSelectedCharacter()
    {
        if (GameState.Instance.CharacterSelected != null)
        {
            ShowDeckScreen.Instance.HideShowDeckScreenButton.gameObject.SetActive(true);
            Show(cardsToDisplay: GameState.Instance.CharacterSelected.CardsInPersistentDeck, $"{GameState.Instance.CharacterSelected.CharacterFullName}", true);
        }
        else
        {
            throw new System.Exception("No character selected!");
        }
    }

    /// <summary>
    /// Uses the currently-selected character.
    /// Example of use: A relic requires you to select a card to attach it to.
    /// </summary>
    public static void ShowMandatorySelectCardFromCharacterDeckScreen(
        Action<AbstractCard> cardSelectHandler,
        Action cannotSelectCardHandler,
        Predicate<AbstractCard> qualificationsForCardSelection,
        string prompt,
        bool canDeclineToChoose = false)
    {
        var cardsInPersistentDeck = GameState.Instance.CharacterSelected.CardsInPersistentDeck;
        var filteredCards = cardsInPersistentDeck.Where(item => qualificationsForCardSelection.Invoke(item));
        if (GameState.Instance.CharacterSelected != null)
        {
            if (filteredCards.IsEmpty())
            {
                cannotSelectCardHandler.Invoke();
                return;
            }
            else
            {
                Card.ClickHandler = (abstractCardClicked) =>
                {
                    Instance.HideShowDeckScreen();
                    cardSelectHandler.Invoke(abstractCardClicked);
                    Card.ClickHandler = null; // at this point we cannot handle any more events.
                };
                ShowDeckScreen.Instance.HideShowDeckScreenButton.gameObject.SetActive(false); // selection is mandatory where possible

            }
            Show(cardsToDisplay: cardsInPersistentDeck,
                prompt,
                canDeclineToChoose);
        }
        else
        {
            throw new System.Exception("No character selected!");
        }
    }

    public static void Show(IEnumerable<AbstractCard> cardsToDisplay, string prompt, bool screenExitAllowed)
    {
        Instance.gameObject.SetActive(true);
        Instance.Populate(cardsToDisplay);
        Instance.HideShowDeckScreenButton.interactable = screenExitAllowed;
        Instance.DisplayText.text = prompt;
    }

    public static void Hide()
    {
        Instance.gameObject.SetActive(false);
        Card.ClickHandler = null;
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
