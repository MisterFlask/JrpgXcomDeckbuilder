using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.UI.Subscreens
{
    public class ShowHideCardInventoryButton : MonoBehaviour
    {

        public void Show()
        {
            SelectCardToAddFromInventoryScreen.ShowCardInventory();
        }

        public void Hide()
        {
            SelectCardToAddFromInventoryScreen.Hide();
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