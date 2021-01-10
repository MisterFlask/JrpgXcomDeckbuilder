using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI.Screens.CampaignScreen
{
    public class PerkDisplayPrefab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public SoldierPerk Perk { get; set; }
        public Image PerkImage;
        public TMPro.TextMeshProUGUI Text;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Perk == null)
            {
                return;
            }

            PerkImage.sprite = Perk.Sprite.ToSprite();
            Text.text = "";
            if (Perk.Stacks != 0)
            {
                Text.text = Perk.Stacks.ToString();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ExplainerPanel.Hide();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ExplainerPanel.ShowPerkHelp(this.Perk);
        }
    }
}