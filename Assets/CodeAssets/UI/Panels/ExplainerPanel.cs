using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExplainerPanel : MonoBehaviour
{
    public static ExplainerPanel INSTANCE;

    public static void Hide()
    {
        INSTANCE.HideTooltip();
    }

    public static void ShowStatusEffectHelp(AbstractStatusEffect effect)
    {
        var description = effect.Description;

        INSTANCE.SetAndShowTooltip(description, title: effect.Name);
    }

    public static void ShowCardHelp(AbstractCard card)
    {
        var description = card.Description();
        INSTANCE.SetAndShowTooltip(description, title: card.Name);
    }

    public CustomGuiText tooltipText;
    // Use this for initialization
    void Start()
    {
        INSTANCE = this;
        ServiceLocator.UtilityObjectHolder.ExplainerPanel = this;
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
