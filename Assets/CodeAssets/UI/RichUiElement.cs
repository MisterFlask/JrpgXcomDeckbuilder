using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ModelShark;
using System.Linq;

/// <summary>
/// This is just a thing for a UI element with an image, background, text, and tooltip
/// </summary>
public class RichUiElement : MonoBehaviour
{
    Text primaryText;
    Image image;
    TooltipTrigger tooltipTrigger;
    Image backgroundImage;

    public string Id { get; set; }
    public string TooltipText { get; set; }
    public string Text { get; set; }

    // Use this for initialization
    void Start()
    {
        this.gameObject.AddComponent<TooltipTrigger>();
        UiElementsManager.AddUiElement(this);
        var textField = tooltipTrigger.parameterizedTextFields.Where(item => item.name == "BodyText").Single();
        this.TooltipText = textField.value;
    }

    // Update is called once per frame
    void Update()
    {
        tooltipTrigger.SetText("BodyText", TooltipText);
        primaryText.text = Text;
    }
}
