using UnityEngine;
using System.Collections;
using HyperCard;
using System.Collections.Generic;

/// <summary>
///  Cards can be dragged here to do things.
/// </summary>
public class CardSelectionArea : MonoBehaviour
{
    List<Card> cardsSelected = new List<Card>();
    public void Init()
    {
        cardsSelected.Clear();
    }

    public void AddCardToSelectionArea(Card card)
    {
        cardsSelected.Add(card);
    }
}
