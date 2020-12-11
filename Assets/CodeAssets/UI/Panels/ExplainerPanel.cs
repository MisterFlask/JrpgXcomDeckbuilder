using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ExplainerPanel : MonoBehaviour
{
    private static string TooltipToDisplay { get; set; }

    public static string DebuggingInfo => ""; //ActionManager.Instance?.GetQueueActionsDebugLogs() ?? "No action manager found";

    public static void Hide()
    {
        TooltipToDisplay = DebuggingInfo;
        // todo
    }

    public static void ShowStatusEffectHelp(AbstractStatusEffect effect)
    {
        var description = effect.Description;
        TooltipToDisplay = description + "\n" + DebuggingInfo;
    }

    public static void ShowCardHelp(AbstractCard card)
    {
        if (card == null)
        {
            return;
        }
        TooltipToDisplay = card.Description() + "\n" + DebuggingInfo;
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

    public static void ShowBattleUnitHelp(BattleUnitPrefab battleUnitPrefab)
    {
        TooltipToDisplay = (battleUnitPrefab.UnderlyingEntity.GetTooltip());
    }
}