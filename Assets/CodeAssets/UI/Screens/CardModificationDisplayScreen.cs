using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using static UnityEngine.UI.Button;
using System;

public class CardModificationDisplayScreen : MonoBehaviour
{
    public static CardModificationDisplayScreen Instance;

    public CardModificationDisplayScreen()
    {
        Instance = this;
    }

    public GameCardDisplay cardDisplayBefore;
    public GameCardDisplay cardDisplayAfter;
    public TMPro.TextMeshProUGUI Title;
    public TMPro.TextMeshProUGUI ConfirmButtonText;
    public Button ConfirmButton;
    public Button CancelButton;
    
    public AbstractCard beforeCard;
    public int? goldCost;
    
    /// <summary>
    /// Note: beforeCard is expected to be in the player's deck.  If it's not, you have a problem.
    /// </summary>
    /// <param name="beforeCard"></param>
    /// <param name="afterCard"></param>
    /// <param name="message"></param>
    /// <param name="goldCost"></param>
    public void Show(AbstractCard beforeCard, AbstractCard afterCard, string message = "Upgrade card?", int? goldCost = null)
    {
        this.gameObject.SetActive(true);
        this.goldCost = goldCost;
        this.beforeCard = beforeCard;
        cardDisplayAfter.GameCard.SetToAbstractCardAttributes(afterCard);
        cardDisplayBefore.GameCard.SetToAbstractCardAttributes(beforeCard);
        Title.text = message;
        ConfirmButtonText.text = GetTextOfConfirmationButton(goldCost);

        ConfirmButton.onClick.RemoveAllListeners();
        CancelButton.onClick.RemoveAllListeners();

        ConfirmButton.onClick.AddListener(() => {
            Hide();

            ActionManager.Instance.UpgradeCard(beforeCard);
            if (goldCost != null)
            {
                GameState.Instance.money -= goldCost.Value;
            }
        });
        CancelButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private string GetTextOfConfirmationButton(int? goldCost)
    {
        if (goldCost == null)
        {
            return "Do it!";
        }
        else
        {
            return $"Purchase [cost: ${goldCost}]";
        }
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
