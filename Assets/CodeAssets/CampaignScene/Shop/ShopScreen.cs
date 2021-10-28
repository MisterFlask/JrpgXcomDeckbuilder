using Assets.CodeAssets.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeAssets.CampaignScene.Shop
{
    public class ShopScreen : EagerMonobehaviour
    {
        public static ShopScreen Instance = null;

        public ShopScreen()
        {
        }

        public int MaxNumCardOffersAvailable => CardOffers.Count;
        public int MaxNumAugmentationOffersAvailable => AugmentationOffers.Count;

        public GameObject CardOfferParent;
        public GameObject AugmeticsParent;
        public Button CancelButton;

        public List<ShopCardOfferPrefab> CardOffers { get; set; }
        public List<ShopAugmentationOfferPrefab> AugmentationOffers { get; set; }

        public bool Initialized = false;

        public void Start()
        {
            Instance = this;

            if (!Initialized)
            {
                CardOffers = CardOfferParent.GetComponentsInChildren<ShopCardOfferPrefab>().ToList();
                AugmentationOffers = AugmeticsParent.GetComponentsInChildren<ShopAugmentationOfferPrefab>().ToList();

                CancelButton.onClick.AddListener(() =>
                {
                    Hide();
                });
                Initialized = true;
            }
        }


        public void Show()
        {
            Start();

            InitCardsInShop();
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }


        public void InitCardsInShop()
        {
            SetItemsInShop(new List<ShopCardOffer>
            {
                new ShopCardOffer()
                {
                    Card = new Bayonet(),
                    Price = 50,
                    Purchased = false
                }
            }, new List<ShopAugmentationOffer>()
            {

            });
        }

        public void SetItemsInShop(
            List<ShopCardOffer> cardsOnSale,
            List<ShopAugmentationOffer> augmentationsOnSale)
        {
            GameState.Instance.ShopData.CardOffers = cardsOnSale;
            GameState.Instance.ShopData.AugmentationOffers = augmentationsOnSale;

            if (cardsOnSale.Count > CardOffers.Count)
            {
                throw new System.Exception("Too any cards on sale; can't hold them all.");
            }
            if (augmentationsOnSale.Count > AugmentationOffers.Count)
            {
                throw new System.Exception("Too many augmentations on sale; can't hold them all");
            }

            int index = 0;
            foreach(var cardOffer in cardsOnSale)
            {
                CardOffers[index].gameObject.SetActive(true);
                CardOffers[index].Offer = cardOffer;
                CardOffers[index].Initialize(cardOffer);
                index++;
            }

            index = 0;

            foreach (var augmentationOffer in augmentationsOnSale)
            {
                AugmentationOffers[index].gameObject.SetActive(true);
                AugmentationOffers[index].ShopOffer = augmentationOffer;
                AugmentationOffers[index].Initialize(augmentationOffer);
                index++;
            }

            // deactivate everything that doesn't have an attached entity
            foreach(var offerPrefab in AugmentationOffers)
            {
                if (offerPrefab.ShopOffer == null)
                {
                    offerPrefab.gameObject.SetActive(false);
                }
            }

            foreach(var offerPrefab in CardOffers)
            {
                if(offerPrefab.Offer == null)
                {
                    offerPrefab.gameObject.SetActive(false);
                }
            }
        }

        public override void AwakenOnSceneStart()
        {
            Instance = this;
        }
    }
}