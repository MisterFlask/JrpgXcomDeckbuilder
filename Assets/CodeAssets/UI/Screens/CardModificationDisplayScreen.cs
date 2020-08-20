using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using static UnityEngine.UI.Button;
using System;

public class CardModificationDisplayScreen : MonoBehaviour
{

    public GameCardDisplay cardDisplayBefore;
    public GameCardDisplay cardDisplayAfter;
    public Text Title;
    public Button ConfirmButton;
    public Button CancelButton;
    Action ThingToDoUponConfirmation;

    public void Populate(ShowingCardModificationMessage message)
    {
        cardDisplayAfter.GameCard.SetToAbstractCardAttributes(message.afterCard);
        cardDisplayBefore.GameCard.SetToAbstractCardAttributes(message.beforeCard);
        Title.text = message.TitleMessage;
        if (!message.CanCancel)
        {
            CancelButton.gameObject.SetActive(false);
        }
        ThingToDoUponConfirmation = message.ThingToDoUponConfirmation;
    }

    // Use this for initialization
    void Start()
    {
        var okButtonClickEvent = new ButtonClickedEvent();
        okButtonClickEvent.AddListener(() => {
            this.GetComponent<Popup>().Close();
            ThingToDoUponConfirmation.Invoke();
        });
        ConfirmButton.onClick = okButtonClickEvent;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
