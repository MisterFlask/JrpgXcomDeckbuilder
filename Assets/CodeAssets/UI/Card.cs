using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Assets.CodeAssets.UI.Tooltips;

namespace HyperCard
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public TextMeshProUGUI description;
        public TextMeshProUGUI title;
        public TextMeshProUGUI energyCost;
        public TextMeshProUGUI cardTags;
        public CardStickerHolder CardStickerHolder;
        public Image CardImage;
        public TooltipTriggerController TooltipController;

        public Image CommonCardFrame;
        public Image UncommonCardFrame;
        public Image RareCardFrame;

        /// <summary>
        /// other purposes, tbd
        /// </summary>
        public Image PurpleCardFrame;
        public Image RedCardFrame;


        public AbstractCard LogicalCard { get; set; }
        public string LogicalCardId { get; set; }

        public bool IsSelected { get; set; } = false;
        public bool TooltipsDisabled { get; set; } = false;

        public void Start()
        {
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
        public void SetCardTopText(string text)
        {
            cardTags.text = text;
        }

        private ProtoGameSprite protoSpriteUsed = null;

        public void Update()
        {
            if (LogicalCard != null)
            {
                Refresh();
                if (protoSpriteUsed != LogicalCard?.ProtoSprite)
                {
                    protoSpriteUsed = LogicalCard?.ProtoSprite;
                    CardImage.sprite = LogicalCard.ProtoSprite.ToSprite();
                }
            }
        }

        public void Refresh()
        {
            this.SetToAbstractCardAttributes(LogicalCard);
            // we'll make this useful later.

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ExplainerPanel.Hide();
            if (BattleScreenPrefab.CardMousedOver == this.LogicalCard)
            {
                BattleScreenPrefab.CardMousedOver = null;
            } 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!TooltipsDisabled)
            {
                TooltipController.ShowTooltipForCard(LogicalCard);
            }

            ExplainerPanel.ShowCardHelp(this.LogicalCard);
            BattleScreenPrefab.CardMousedOver = this.LogicalCard;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ClickHandler?.Invoke(this.LogicalCard);
        }

        public static Action<AbstractCard> ClickHandler { get; set; }
    }

}
