using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProductionActionButton : MonoBehaviour
{
    public Button button;
    public TMPro.TextMeshProUGUI text;

    public static ProductionActionButton INSTANCE;

    public void HandleOnMouseButtonUpEvent()
    {
        Debug.Log("Mouse up event on Production Action Button");
        ServiceLocator.GetActionManager().PerformProductionActionOnCardSelectedIfPossible();
    }


    // Use this for initialization
    void Start()
    {
        INSTANCE = this;
    }

    // Update is called once per frame
    void Update()
    {
        var card = ServiceLocator.GetGameStateTracker().GetCardSelected();
        if (card == null)
        {
            this.text.text = "No Production Action";
            return;
        }
        var action = card.LogicalCard.GetApplicableProductionAction();
        if (action == ProductionAction.UPGRADE_CARD)
        {
            this.text.text = $"Upgrade Card [requires {card.LogicalCard.ProductionCostForNextUpgrade()}]";
        }
        else
        {
            this.text.text = "No Production Action";
        }
    }
}
