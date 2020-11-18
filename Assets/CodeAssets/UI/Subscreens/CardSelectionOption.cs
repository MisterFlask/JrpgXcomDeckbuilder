using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using HyperCard;
using UnityEngine.UI;

public class CardSelectionOption : MonoBehaviour, IPointerClickHandler
{
    // set via unity
    public Card card;
    public CardSelectionButton CardSelectButton;

    private AbstractCard cardLogic { get; set; }

    public void Start()
    {
        Require.NotNull(card);
        Require.NotNull(CardSelectButton);
    }

    public void Initialize(AbstractCard abstractCard)
    {
        card.SetToAbstractCardAttributes(abstractCard);
        cardLogic = abstractCard;
        Debug.Log("Initialized card selection option");
    }


    public void OnPointerClick(PointerEventData eventData)
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


}
