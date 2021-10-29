using Assets.CodeAssets.Utils;
using HyperCard;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI.Screens.BattleScreen
{
    public class SelectCardInHandOverlay : EagerMonobehaviour
    {
        public static SelectCardInHandOverlay INSTANCE;
        public SelectCardInHandOverlay()
        {
        }

        public TMPro.TextMeshProUGUI SelectInstructions;
        public Button ConfirmButton;
        public RectTransform SelectedCardHolder;

        public bool IsCardSelectBehaviorActive { get; set; } = false;
        public SelectCardInHandBehavior BehaviorActive { get; set; } = null;
        public CardSelectionFuture CurrentFuture { get; set; }

        public Action OnFinished { get; set; } = () => { };


        public static void ShowPromptForCardSelection(SelectCardInHandBehavior behavior, CardSelectionFuture future)
        {
            INSTANCE.BehaviorActive = behavior;
            INSTANCE.gameObject.SetActive(true);
            INSTANCE.IsCardSelectBehaviorActive = true;
            INSTANCE.CurrentFuture = future;
        }

        public static void Hide()
        {
            INSTANCE.gameObject.SetActive(false);
            CardAnimationManager.INSTANCE.cardsInHand.ForEach((item) =>
            {
                // deselect each card so it goes to the appropriate place.
                item.IsSelected = false;
            });
            INSTANCE.IsCardSelectBehaviorActive = false;
            INSTANCE.CurrentFuture = null;
        }

        // Use this for initialization
        void Start()
        {
            INSTANCE = this;
            ConfirmButton.onClick.AddListener(() =>
            {
                if (INSTANCE.BehaviorActive == null)
                {
                    Debug.Log("Failed to submit cards; no behavior selected");
                    Hide();
                    return;
                }

                if (BehaviorActive.IsAcceptableToSubmit())
                {
                    Submit();
                    Hide();
                }
                else
                {
                    Debug.Log("Attempted to submit invalid number of cards");
                }
           });

            SelectInstructions.text = "Select card to discard.";
        }

        // Update is called once per frame
        void Update()
        {
            if (BehaviorActive == null)
            {
                return;
            }
            SelectInstructions.text = $"{BehaviorActive.SelectCardsDisplayText} [{CardAnimationManager.SelectedCardsInHand.Count} selected]";
        }

        private static void Submit()
        {
            var cardsSelected = CardAnimationManager.INSTANCE.cardsInHand
                .Where(item => item.IsSelected)
                .Select(item => item.LogicalCard).ToList();
            INSTANCE.BehaviorActive.SubmitCards(cardsSelected);
            INSTANCE.CurrentFuture.CardsSelected = cardsSelected;
        }

        public void ExecuteClickingOnCard(CardUiBehaviors cardUiBehaviorNullable)
        {
            if (this.BehaviorActive == null)
            {
                Debug.Log("No behavior active; closing overlay");
                Hide();
            }
        }

        public override void AwakenOnSceneStart()
        {
            INSTANCE = this;
        }
    }
}