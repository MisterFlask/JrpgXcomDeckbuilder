using HyperCard;
using ModelShark;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.UI.Tooltips
{
    public class TooltipWithACard : MonoBehaviour
    {
        public Card Card;

        public static AbstractCard CARD_TO_DISPLAY = null;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (CARD_TO_DISPLAY != null)
            {
                Card.LogicalCard = CARD_TO_DISPLAY;
            }
        }
    }
}