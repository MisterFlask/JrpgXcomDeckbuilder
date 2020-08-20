using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExplainerPanel : MonoBehaviour
{
    public CustomGuiText tooltipText;
    // Use this for initialization
    void Start()
    {
        ServiceLocator.private_ExplainerPanel = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetAndShowTooltip(string description, string title = "")
    {
        gameObject.SetActive(true);
        tooltipText.SetText(description);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
