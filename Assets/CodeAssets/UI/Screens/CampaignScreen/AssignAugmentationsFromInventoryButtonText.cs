using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.UI.Screens.CampaignScreen
{
    public class AssignAugmentationsFromInventoryButtonText : MonoBehaviour
    {

        public TMPro.TextMeshProUGUI Text;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GameState.Instance?.CardInventory == null)
            {
                return;
            }

            var cardsInInventory = GameState.Instance.AugmentationInventory.Count;
            Text.text = $"Assign Card From Inventory [{cardsInInventory}]";
        }
    }
}