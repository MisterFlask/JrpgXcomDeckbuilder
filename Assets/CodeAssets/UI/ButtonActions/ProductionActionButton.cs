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
    }
}
