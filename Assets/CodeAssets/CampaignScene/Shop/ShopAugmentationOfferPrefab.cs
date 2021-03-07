using HyperCard;
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
        public ShopAugmentationOffer ShopOffer { get; set; }

        public bool IsActive => ShopOffer != null;


        public void OnPointerClick(PointerEventData eventData)
        {
            if (GameState.Instance.money < this.ShopOffer.Price)
            {
                // TODO:  "You broke?"
                return;
            }

            // purchase logic
            GameState.Instance.AugmentationInventory.Add(this.ShopOffer.Augmentation);
            GameState.Instance.money -= this.ShopOffer.Price;
            GameState.Instance.ShopData.AugmentationOffers.Remove(this.ShopOffer);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ExplainerPanel.ShowRawTextHelp($"<color=white>{ShopOffer.Augmentation.Name()}:  </color><color=grey>{ShopOffer.Augmentation.Description()}</color>");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ExplainerPanel.Hide();
        }

        // Use this for initialization
        void Start()
        {

        }
        public void Initialize(ShopAugmentationOffer offer)
        {
            ShopOffer = offer;
            Image.sprite = offer.Augmentation.Sprite.ToSprite();
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
