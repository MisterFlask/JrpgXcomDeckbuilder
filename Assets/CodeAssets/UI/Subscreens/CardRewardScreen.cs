using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.CodeAssets.Utils;

public class CardRewardScreen : EagerMonobehaviour
{
    public static CardRewardScreen Instance;

    public void Awake()
    {
    }


    public Transform CardGrid;
    public CardSelectionOption CardTemplate;
    public Button DeclineButton;

    private List<GameObject> CardsDisplayed = new List<GameObject>();

    public static void Hide()
    {
        Instance.gameObject.SetActive(false);
    }

    public void Show(List<AbstractCard> cardsToDisplay, AbstractBattleUnit unitFor)
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
            var cardClone = CardTemplate.Spawn(CardGrid.transform);
            cardClone.gameObject.SetActive(true);
            cardClone.Initialize(card, unitFor);
            // CardTemplate.CardSelectionScreen = this.GetComponent<Popup>();
            var hypercard = cardClone.card;
            hypercard.SetToAbstractCardAttributes(card);
            cardClone.transform.SetParent(CardGrid.gameObject.transform);
            cardClone.transform.localScale = Vector3.one;
            CardsDisplayed.Add(cardClone.gameObject);
        }
    }


    public void Start()
    {
        Log.Info("new card reward screen: " + this.gameObject.name + $" [child of {this.gameObject.transform.parent.name}]");

        DeclineButton.onClick.AddListener(() => {
            Hide();
        });
    }

    public override void AwakenOnSceneStart()
    {
        Instance = this;
    }
}
