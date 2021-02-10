using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using static UnityEngine.UI.Button;
using System;
using System.Linq;

public class SelectCardsFromHandDisplayScreen : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Title;
    public Button button;
    public CardSelectionArea cardSelectionArea;

    public int minCardsToSelect;
    public int? maxCardsToSelect;

    public void Populate(string title = "Select a card", int minNumberCardsToSelect = 0, int? maxNumberOfCardsToSelect = null)
    {
        Title.text = title;
        minCardsToSelect = minNumberCardsToSelect;
        maxCardsToSelect = maxNumberOfCardsToSelect;
    }

    // Use this for initialization
    void Start()
    {
        try { 
        Require.NotNull(Title);
        Require.NotNull(button);
        Require.NotNull(cardSelectionArea);
        }catch(Exception e)
        {
            string parentName = this.transform?.parent?.gameObject?.name;
            throw e;
        }
        var okButtonClickEvent = new ButtonClickedEvent();
        okButtonClickEvent.AddListener(() => {
            var cardsSelected = ServiceLocator.GetCardAnimationManager()
                .cardsInHand
                .Where(item => item.IsSelected).ToList();
            if (cardsSelected.Count >= minCardsToSelect && cardsSelected.Count <= (maxCardsToSelect ?? 100))
            {
                // TODO: Add actual logic for this
                this.GetComponent<Popup>().Close();
                ServiceLocator.GetUiStateManager().CurrentCardSelectingUiState.HandleConfirmationEvent(cardsSelected.ToList());
                ServiceLocator.GetUiStateManager().CurrentCardSelectingUiState = new NormalBoardUiState();
                ServiceLocator.GetActionManager().IsCurrentActionFinished = true;
            }
            else
            {
                /// should have some kind of error messaging here.  Later!
            }

        });
        button.onClick = okButtonClickEvent;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
