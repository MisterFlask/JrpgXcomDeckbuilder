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
        public GameObject CardSoldSticker;
        public Button PurchaseButton;

        public ShopCardOffer Offer { get; set; }

        public bool IsActive => Offer != null;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (GameState.Instance.Credits < this.Offer.Price)
            {
                // TODO:  "You broke?"
                return;
            }

            // purchase logic; I should move this elsewhere.
            GameState.Instance.CardInventory.Add(this.Card.LogicalCard);
            GameState.Instance.Credits -= this.Offer.Price;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ExplainerPanel.ShowRawTextHelp($"<color=white>{Offer.Card.Name}:  </color><color=grey>{Offer.Card.DescriptionInner()}</color>");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ExplainerPanel.Hide();
        }

        // Use this for initialization
        void Start()
        {

        }

        public void Initialize(ShopCardOffer offer)
        {
            Offer = offer;
            Card.SetToAbstractCardAttributes(offer.Card);
            PurchaseButton.onClick.AddListener(() => {
                PurchaseButton.interactable = false;
                GameState.Instance.CardInventory.Add(this.Card.LogicalCard);
                GameState.Instance.ShopData.CardOffers.Remove(Offer);
            });
        }
        // Update is called once per frame
        void Update()
        {
            if (Card == null)
            {
                return;
            }
            PriceText.text = "" + Offer.Price;
        }
    }
}