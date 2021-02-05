using Assets.CodeAssets.CampaignScene.Shop;
using Assets.CodeAssets.UI.Subscreens;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.UI.ButtonActions
{
    public class ShowHideAugmentationInventoryButton : MonoBehaviour
    {
        public void ShowOnClick()
        {
            AssignFreeAugmentationsPanel.Instance.Show();
        }

        public void HideOnClick()
        {
            AssignFreeAugmentationsPanel.Instance.Hide();
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