using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HyperCard;
using System.Linq;

public abstract class CardSelectingBoardUiState
{
    // Probably only true for typical board UI state.
    public bool AllowsNormalActions = false;
    public string Name = "Select Cards";
    public int NumCardsToSelect = 1;

    protected virtual void HandleCardReleasedEventForCardSelect(List<GameObject> elements, AbstractCard logicalCard)
    {
        var card = ServiceLocator.GetCardAnimationManager().GetGraphicalCard(logicalCard);
        card.IsSelected = !card.IsSelected;
        ServiceLocator.GetCardAnimationManager().ReorientAllCards();
    }

    public void HandleCardReleasedEvent(List<GameObject> elements, AbstractCard logicalCard)
    {
        if (!AllowsNormalActions)
        {
            HandleCardReleasedEventForCardSelect(elements, logicalCard);
        }
    }

    public virtual void HandleConfirmationEvent(List<Card> cardsSelected)
    {

    }
}
