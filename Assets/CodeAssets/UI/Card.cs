using UnityEngine;
using System.Collections;
using TMPro;
using LivelyTextGlyphs;
using System.Collections.Generic;
using System;

namespace HyperCard
{
    public class Card : MonoBehaviour
    {
        public TextMeshProUGUI description;
        public TextMeshProUGUI title;
        public TextMeshProUGUI energyCost;
        public TextMeshProUGUI cardTags;
        public GameObject MightDisplayTooltipParent;
        public GameObject OtherDisplayTooltipParent;
        public LTText MightDisplayTooltipText;
        public LTText OtherDisplayTooltipText;
        public TextMeshProUGUI powerValueText;
        public TextMeshProUGUI toughnessValueText;
        public GameObject powerValuePrefab;
        public GameObject toughnessValuePrefab;

        public AbstractCard LogicalCard { get; set; }

        public bool IsMovedToSelectionArea { get; set; } = false;

        public void Start()
        {
            HideTooltips();
        }

        public void ShowMightTooltip(List<EffectiveMightComponent> mightComponents)
        {
            MightDisplayTooltipText.SetText(GetTextOfMightTooltip(mightComponents));
            MightDisplayTooltipParent.SetActive(true);
        }

        public string GetTextOfMightTooltip(List<EffectiveMightComponent> mightComponents)
        {
            var desc = "";
            foreach(var item in mightComponents)
            {
                desc += item.FormattedString() + "\n";
            }
            return desc;
        }

        public void ShowOtherTooltips(string s)
        {
            OtherDisplayTooltipText.SetText(s);
            OtherDisplayTooltipParent.SetActive(true);
        }

        public void HideTooltips()
        {
            MightDisplayTooltipParent.SetActive(false);
            OtherDisplayTooltipParent.SetActive(false);
        }

        public void SetCardTitle(string text)
        {
            title.text = text;
        }
        public void SetCardDescription(string text)
        {
            description.text = text;
        }
        public void SetCardEnergyCost(int text)
        {
            energyCost.text = text.ToString();
        }
        public void SetCardTags(string text)
        {
            cardTags.text = text;
        }

        public void Refresh()
        {
            this.SetToAbstractCardAttributes(LogicalCard);
        }

        internal void SetPowerToughness(AbstractCard abstractCard)
        {
            if (!abstractCard.IsLegion())
            {
                powerValuePrefab.SetActive(false);
                toughnessValuePrefab.SetActive(false);
                return;
            }

            this.powerValueText.SetText(abstractCard.BasePower.ToString());
            this.toughnessValueText.SetText(abstractCard.CurrentToughness.ToString());
        }
    }

}
