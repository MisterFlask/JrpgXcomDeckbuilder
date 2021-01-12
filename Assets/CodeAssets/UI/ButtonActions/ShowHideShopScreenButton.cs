using Assets.CodeAssets.CampaignScene.Shop;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.UI.ButtonActions
{
    public class ShowHideShopScreenButton : MonoBehaviour
    {

        public void ShowOnClick()
        {
            ShopScreen.Instance.Show();
        }

        public void HideOnClick()
        {
            ShopScreen.Instance.Hide();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}