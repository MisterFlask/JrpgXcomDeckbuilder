using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DeployCardButton : MonoBehaviour
{
    private Color initialNormalColor;
    public void HandleOnMouseButtonUpEvent()
    {
        Debug.Log("Mouse up event on Deploy Card Button");
        // ServiceLocator.GetActionManager().PlayCardSelectedIfApplicable(ServiceLocator.GetGameStateTracker().GetCardSelected());
    }


    // Use this for initialization
    void Start()
    {

        var button = this.GetComponent<Button>();
        var block = button.colors;
        this.initialNormalColor = block.normalColor;
    }

    // Update is called once per frame
    void Update()
    { 
        var cardSelected = ServiceLocator.GetGameStateTracker().GetCardSelected();
        var deployableCardSelected = cardSelected != null && cardSelected.LogicalCard.CanPlay();
        if (deployableCardSelected)
        {
            var button = this.GetComponent<Button>();
            var block = button.colors;
            var lerpedColor = Color.Lerp(Color.red, Color.yellow, Mathf.PingPong(Time.time, .5f));
            block.normalColor = lerpedColor;
            button.colors = block;
        }
        else
        {

            var button = this.GetComponent<Button>();
            var block = button.colors;
            block.normalColor = this.initialNormalColor;
            button.colors = block;
        }
    }
}