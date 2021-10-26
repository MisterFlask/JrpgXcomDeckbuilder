using HyperCard;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI.CardParts
{
    public class CardVisualTagPrefab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TMPro.TextMeshProUGUI text;
        public Image image;

        public CardVisualTag CardTag { get; set; }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (CardTag == null)
            {
                return;
            }
            image.sprite = CardTag.ProtoSprite.ToSprite();
            text.text = CardTag.Text;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (CardTag == null)
            {
                return;
            }
            ExplainerPanel.ShowRawTextHelp(CardTag.Tooltip);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ExplainerPanel.Hide();
        }
    }

    public class CardVisualTag
    {
        public ProtoGameSprite ProtoSprite { get; set; }
        public string Text { get; set; } = "";
        public string Tooltip { get; set; } = "";
        public CardVisualTagPrefab CorrespondingPrefab { get; set; }

        ///

        public static CardVisualTag AttackIcon = new CardVisualTag
        {
            ProtoSprite = GameIconProtoSprite.VisualTagIcon("attack-icon"),
            Text = "",
            Tooltip = "This card is an attack."
        };
        public static CardVisualTag SkillIcon = new CardVisualTag
        {
            ProtoSprite = GameIconProtoSprite.VisualTagIcon("skill-icon"),
            Text = "",
            Tooltip = "This card is a skill."
        };
        public static CardVisualTag PowerIcon = new CardVisualTag
        {
            ProtoSprite = GameIconProtoSprite.VisualTagIcon("power-icon"),
            Text = "",
            Tooltip= "This card is a power."
        };
    }


}