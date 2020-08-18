using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class UiStateMessage
{
    public string Name { get; set;}

}

public class ShowingMapMessage : UiStateMessage
{
    public ShowingMapMessage()
    {
        Name = nameof(ShowingMapMessage);
    }

}

public class ShowingCardModificationMessage : UiStateMessage
{
    public AbstractCard beforeCard;
    public AbstractCard afterCard;

    public string TitleMessage;
    public Action ThingToDoUponConfirmation = () => { };
    // If the player's allowed to cancel, this is populated
    public bool CanCancel = false;

    public ShowingCardModificationMessage()
    {
        Name = nameof(ShowingCardModificationMessage);
    }
}

public class ShowingCardsMessage : UiStateMessage
{
    public ShowingCardsMessage()
    {
        Name = nameof(ShowingCardsMessage);
    }

    public List<AbstractCard> CardsToShow = new List<AbstractCard>();
}
public class ShowSelectCardToAddScreenMessage : UiStateMessage
{
    public ShowSelectCardToAddScreenMessage()
    {
        Name = nameof(ShowSelectCardToAddScreenMessage);
    }

    public List<AbstractCard> CardsToShow = new List<AbstractCard>();
}