using Assets.CodeAssets.GameLogic;
using HyperCard;
using ModelShark;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI.Tooltips
{
    [RequireComponent(typeof(TooltipTrigger))]
    public class TooltipTriggerController : MonoBehaviour
    {

        public TooltipTrigger CardTooltipTrigger => this.GetComponent<TooltipTrigger>();

        private Card TooltipCard => CardTooltipTrigger?.tooltipStyle?.GetComponent<TooltipWithACard>()?.Card;

        public void Start()
        {
        }

        public void ShowTooltipForBattleUnitClass(AbstractBattleUnit unit)
        {
            if (unit == null)
            {
                return;
            }
            if (unit.IsAlly && unit.SoldierClass != null)
            {
                RefreshTooltip(cardReferenced: null,
                    title: "Reference",
                    description: $"<b><color=green>{unit.SoldierClass.Name()}</color></b>\n{unit.SoldierClass.Description()}");
            }
            else
            {
                RefreshTooltip(cardReferenced: null,
                    title: "Reference",
                    description: "Flavor text for enemy!");
            }
        }

        public void ShowTooltipForStatusEffect(AbstractStatusEffect effect)
        {
            RefreshTooltip(cardReferenced: effect.ReferencedCard,
                title: "Reference",
                description: $"<color=green>{effect.Name}</color>:{effect.Description}");
        }

        public void ShowTooltipForCard(AbstractCard card)
        {
            if (card == null)
            {
                Log.Error("Could not show tooltip for card.  Card was null.");
                return;
            }

            RefreshTooltip(
                cardReferenced: card.ReferencesAnotherCard,
                title: "References", 
                description: $"{MagicWord.GetFormattedMagicWordsForCard(card)}");
        }

        public void RefreshTooltip(
            string title = null,
            AbstractCard cardReferenced = null,
            string description = null)
        {
            if (cardReferenced == null && string.IsNullOrEmpty(description))
            {
                CardTooltipTrigger.enabled = false;
            }
            else
            {
                CardTooltipTrigger.enabled = true;
            }


            TooltipWithACard.CARD_TO_DISPLAY = cardReferenced;
            if (title != null)
            {
                CardTooltipTrigger.SetText("TitleText", title);
            }
            else
            {
                CardTooltipTrigger.SetText("TitleText", "");
            }

            if (cardReferenced != null)
            {
                CardTooltipTrigger.TurnSectionOn("CardPrefab");
                TooltipCard.name = $"Tooltip card for {cardReferenced.Name}";
                TooltipCard.LogicalCard = cardReferenced;
                TooltipCard.TooltipsDisabled = true;
                TooltipCard.Refresh();
            }
            else
            {
                CardTooltipTrigger.TurnSectionOff("CardPrefab");
            }

            if (description != null && !description.IsEmpty())
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