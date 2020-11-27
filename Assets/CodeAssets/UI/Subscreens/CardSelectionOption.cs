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
    public bool WasInitialized = false;

    public AbstractBattleUnit unit { get; set; }
    public AbstractCard cardLogic { get; set; }

    public void Start()
    {
        Require.NotNull(card);
        Require.NotNull(CardSelectButton);
    }

    public void Initialize(AbstractCard abstractCard, AbstractBattleUnit unit)
    {
        WasInitialized = true;
        Require.NotNull(abstractCard);
        Require.NotNull(unit);

        card.SetToAbstractCardAttributes(abstractCard);
        cardLogic = abstractCard;
        this.unit = unit;
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
