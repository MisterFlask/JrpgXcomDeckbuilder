﻿using HyperCard;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.CodeAssets.CampaignScene.Shop
{
    public class ShopAugmentationOfferPrefab : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Image Image;
        public TMPro.TextMeshProUGUI PriceText;
        public AbstractShopAugmentationOffer ShopOffer { get; set; }

        public bool IsActive => ShopOffer != null;



        public void OnPointerClick(PointerEventData eventData)
        {
            // purchase logic
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ExplainerPanel.ShowRawTextHelp($"<color=white>{ShopOffer.Augmentation.Title}:  </color><color=grey>{ShopOffer.Augmentation.Description}</color>");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ExplainerPanel.Hide();
        }

        // Use this for initialization
        void Start()
        {

        }
        public void Initialize(AbstractShopAugmentationOffer offer)
        {
            ShopOffer = offer;
            Image.sprite = offer.Augmentation.ProtoSprite.ToSprite();
        }
        // Update is called once per frame
        void Update()
        {
            if (ShopOffer == null)
            {
                return;
            }
            PriceText.text = "" + ShopOffer.Price;
            
        }
    }
}
