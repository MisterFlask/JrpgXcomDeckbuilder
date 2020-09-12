using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

namespace HyperCard
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TextMeshProUGUI description;
        public TextMeshProUGUI title;
        public TextMeshProUGUI energyCost;
        public TextMeshProUGUI cardTags;
        public GameObject MightDisplayTooltipParent;
        public GameObject OtherDisplayTooltipParent;
        public CustomGuiText MightDisplayTooltipText;
        public CustomGuiText OtherDisplayTooltipText;
        public TextMeshProUGUI powerValueText;
        public TextMeshProUGUI toughnessValueText;
        public GameObject powerValuePrefab;
        public GameObject toughnessValuePrefab;

        public AbstractCard LogicalCard { get; set; }
        public string LogicalCardId { get; set; }

        public bool IsMovedToSelectionArea { get; set; } = false;

        public void Start()
        {
            HideTooltips();
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

        public void OnPointerExit(PointerEventData eventData)
        {
            ExplainerPanel.Hide();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ExplainerPanel.ShowCardHelp(this.LogicalCard);
        }
    }

}
