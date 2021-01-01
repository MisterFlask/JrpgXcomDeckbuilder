using HyperCard;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.CodeAssets.CampaignScene.Shop
{
    public class ShopCardOfferPrefab : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public TMPro.TextMeshProUGUI PriceText;
        public Card Card; 

        public AbstractShopCardOffer Offer { get; set; }

        public bool IsActive => Offer != null;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            // purchase logic
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ExplainerPanel.ShowRawTextHelp($"<color=white>{Offer.Card.Name}:  </color><color=grey>{Offer.Card.Description()}</color>");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ExplainerPanel.Hide();
        }

        // Use this for initialization
        void Start()
        {

        }

        public void Initialize(AbstractShopCardOffer offer)
        {
            Offer = offer;
            Card.SetToAbstractCardAttributes(offer.Card);
        }
        // Update is called once per frame
        void Update()
        {
            if (Card == null) return;
            PriceText.text = "" + Offer.Price;
        }
    }
}