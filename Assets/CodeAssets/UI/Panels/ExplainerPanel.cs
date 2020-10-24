using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ExplainerPanel : MonoBehaviour
{
    private static string TooltipToDisplay { get; set; }

    public static void Hide()
    {
        TooltipToDisplay = "";
        // todo
    }

    public static void ShowStatusEffectHelp(AbstractStatusEffect effect)
    {
        var description = effect.Description;
        TooltipToDisplay = description;
    }

    public static void ShowCardHelp(AbstractCard card)
    {
        if (card == null)
        {
            return;
        }
        TooltipToDisplay = card.Description();
    }

    public CustomGuiText tooltipText;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SetAndShowTooltip(description: TooltipToDisplay);
    }

    public void SetAndShowTooltip(string description, string title = "")
    {
        tooltipText.SetText(description);
    }

    public void HideTooltip()
    {
        // todo
    }
}