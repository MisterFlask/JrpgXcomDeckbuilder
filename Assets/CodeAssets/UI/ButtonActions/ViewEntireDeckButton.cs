using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.EventSystems;

public class ViewEntireDeckButton : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("View entire deck button clicked");
        var deck = ServiceLocator.GetGameStateTracker().Deck.PersistentDeckList;
        ServiceLocator.GetUiStateManager().SwitchToUiState(new ShowingCardsMessage { CardsToShow = deck.ToList() });
    }
}
                                                                                                                                                                                                       