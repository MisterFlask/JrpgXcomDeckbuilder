using HyperCard;
using ModelShark;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI.Tooltips
{
    public class TooltipTriggerController : MonoBehaviour
    {

        public TooltipTrigger CardTooltipTrigger;

        private Card TooltipCard => CardTooltipTrigger?.tooltipStyle?.GetComponent<TooltipWithACard>()?.Card;

        public void Start()
        {
        }

        public void ShowTooltipForCard(AbstractCard card)
        {
            RefreshTooltip(card: null, title: "References Card", description: $"Description for {card.Name}");
        }

        public void RefreshTooltip(
            string title = null,
            AbstractCard card = null,
            string description = null)
        {

            TooltipWithACard.CARD_TO_DISPLAY = card;
            if (title != null)
            {
                CardTooltipTrigger.SetText("TitleText", title);
            }
            else
            {
                CardTooltipTrigger.TurnSectionOff("TitleText");
            }

            if (card != null)
            {
                CardTooltipTrigger.TurnSectionOn("CardPrefab");
                TooltipCard.name = $"Tooltip card for {card.Name}";
                TooltipCard.LogicalCard = card;
                TooltipCard.TooltipsDisabled = true;
                TooltipCard.Refresh();
            }
            else
            {
                CardTooltipTrigger.TurnSectionOff("CardPrefab");
            }

            if (description != null)
            {
                CardTooltipTrigger.TurnSectionOn("DescriptionTextSection");
                CardTooltipTrigger.SetText("DescriptionText", description);
            }
            else
            {
                CardTooltipTrigger.TurnSectionOff("DescriptionTextSection");
            }

        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}