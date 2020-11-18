using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UiStateManager : MonoBehaviour
{
    public GameObject[] MapParentObjects;
    public GameObject[] CardDisplayObjects;
    public GameObject[] SelectCardToAddDisplayObjects;

    public CardSelectingBoardUiState CurrentCardSelectingUiState { get; set; } = new NormalBoardUiState();

    private bool Initialized = false;

    private Dictionary<string, GameObject[]> StateToGameObjectAssociations = new Dictionary<string, GameObject[]>();

    //private Dictionary<string, Popup> StateToPopupAssociations = new Dictionary<string, Popup>();

    public GameObject DeckDisplayPopup;
    public GameObject SelectCardToAddPopup;
    //public Popup SelectCardsFromHandDisplayScreenPopup;

    public CardModificationDisplayScreen ShowCardModifiedScreen;
    private Canvas m_canvas;

    public GameObject popupBackground = null;

    public DilemmaScreen DilemmaScreenTemplate;

    public void Update()
    {
        /// this is just a hack to ensure that all start functions got called
        if (!Initialized && ScriptExecutionTracker.IsUiStateManagerReadyToDisableGameobjects)
        {
            StateToGameObjectAssociations[nameof(ShowingMapMessage)] = MapParentObjects;
            StateToGameObjectAssociations[nameof(ShowingCardsMessage)] = CardDisplayObjects;
            StateToGameObjectAssociations[nameof(ShowSelectCardToAddScreenMessage)] = SelectCardToAddDisplayObjects;


            m_canvas = GameObject.Find("UI_CANVAS").GetComponent<Canvas>();

            Debug.Log("Initialized ui state manager");
            Initialized = true;
        }
    }

    public void PromptPlayerForCardSelection(CardSelectingBoardUiState state) 
    {
        this.CurrentCardSelectingUiState = state;
        // Now, we must:
        // 1:  Put up a popup overlay to prevent non-sovereign actions
        // 2:  Move cards above this overlay
        // 3:  Make visible the Confirm button, the Title of the action ("Discard a card"),
        // and the area where the user is allowed to drop the card.

        //var popupPrefab = OpenPopup(SelectCardsFromHandDisplayScreenPopup.gameObject);
        //var showDeckScreen = popupPrefab.GetComponent<SelectCardsFromHandDisplayScreen>();

        //showDeckScreen.Populate(title: state.Name, minNumberCardsToSelect: state.NumCardsToSelect);
        ServiceLocator.GetUiCanvas().SortChildrenBasedOnSortableUiElements();
    }

}
