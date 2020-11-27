using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using HyperCard;

public class CardSelectionButton : MonoBehaviour, IPointerClickHandler
{
    public CardSelectionOption option;

    public void OnPointerClick(PointerEventData eventData)
    {
        var cardLogic = option.card.LogicalCard;
        Debug.Log("Added card to hand!");
        ServiceLocator.GetActionManager().AddCardToPersistentDeck(cardLogic.CopyCard(), option.unit);
        ServiceLocator.GetActionManager().IsCurrentActionFinished = true; // Done to declare that this action has been addressed for animation purposes
        CardRewardScreen.Hide();
    }

    // Use this for initialization
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {

    }
}
