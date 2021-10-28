using Assets.CodeAssets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI.Subscreens
{

    public class SelectCardToAddFromInventoryScreen : EagerMonobehaviour
    {

        public override void AwakenOnSceneStart()
        {
            Instance = this;
        }
        public SelectCardToAddFromInventoryScreen()
        {

        }

        public void Awake()
        {
        }

        public void Start()
        {
            Instance = this;
        }

        public GridLayoutGroup CardParent;
        public AssignCardDisplay CardTemplate;


        private List<GameObject> CardsDisplayed = new List<GameObject>();

        private static SelectCardToAddFromInventoryScreen Instance;
        

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

        public static void ShowCardInventory()
        {
            Show(GameState.Instance.CardInventory);
        }

        public static void Show(IEnumerable<AbstractCard> cardsToDisplay)
        {
            Instance.Populate(cardsToDisplay, GameState.Instance.CharacterSelected);
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

        private void Populate(IEnumerable<AbstractCard> cardsToDisplay, AbstractBattleUnit battleUnit)
        {
            Require.NotNull(cardsToDisplay);
            var displaysCreated = CardPresentationUtil.PopulateCards(cardsToDisplay, CardsDisplayed, CardTemplate.gameObject, CardParent.gameObject);
            foreach(var display in displaysCreated)
            {
                display.GetComponent<AssignCardDisplay>().Init(battleUnit, display.GameCard.LogicalCard);
            }
        }
    }
}