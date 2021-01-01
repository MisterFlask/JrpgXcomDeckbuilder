using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ExplainerPanel : MonoBehaviour
{
    private static string TooltipToDisplay { get; set; }
    const string Bullet = "\u2022";
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

    internal static void ShowRawTextHelp(string v)
    {
        TooltipToDisplay = v;
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
        if (battleUnitPrefab == null || battleUnitPrefab.UnderlyingEntity == null)
        {
            return;
        }
        var entity = battleUnitPrefab.UnderlyingEntity;

        string nameTooltip = $"<color=yellow>{entity.CharacterName}</color>" + Newline();
        string intentTooltip = "";
        if (battleUnitPrefab.UnderlyingEntity.CurrentIntents != null)
        {
            intentTooltip = "<color=red>Intent:</color>";

            foreach (var intent in battleUnitPrefab.UnderlyingEntity.CurrentIntents)
            {
                intentTooltip += Environment.NewLine + intent.GetGenericDescription();
            }
        }
        var basicTooltip = nameTooltip+intentTooltip;

        // now, for each attribute, say what the attribute does

        if (!battleUnitPrefab.UnderlyingEntity.StatusEffects.IsEmpty())
        {
            basicTooltip += Newline();
            basicTooltip += "<color=red>Status Effects:</color>" + Newline();
            foreach (var effect in battleUnitPrefab.UnderlyingEntity.StatusEffects)
            {
                basicTooltip += Newline();
                basicTooltip += ListItemFormat(H1Format($"{effect.Name}[{effect.Stacks}]") + ":  " + effect.Description);
            }
        }
        TooltipToDisplay = (basicTooltip);

    }
    public static string Newline()
    {
        return Environment.NewLine;
    }
    public static string H1Format(string text)
    {
        return $"<color=green>{text}</color>";
    }
    public static string ListItemFormat(string text)
    {
        return $"{Bullet}{text}";
    }


}