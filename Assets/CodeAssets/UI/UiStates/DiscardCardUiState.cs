using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HyperCard;

public class DiscardCardUiState : CardSelectingBoardUiState
{
    public DiscardCardUiState(int numCardsToDiscard = 1, string message = "Discard a card.")
    {
        this.AllowsNormalActions = false;
        NumCardsToSelect = numCardsToDiscard;
        Name = message;
    }

    public override void HandleConfirmationEvent(List<Card> cardsSelected)
    {
        foreach(var card in cardsSelected)
        {
            ServiceLocator.GetActionManager().DiscardCard(card.LogicalCard);
        }
    }
}
