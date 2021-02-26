using HyperCard;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI.Subscreens
{
    [RequireComponent(typeof(GameCardDisplay))]
    public class AssignCardDisplay : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI CardNotAvailableText;
        public Button SelectCardButton;

        GameCardDisplay GameCardDisplay => this.GetComponent<GameCardDisplay>();

        private bool IsTaken { get; set; }
        private AbstractCard Card { get; set; }
        private AbstractBattleUnit UnitSelected { get; set; }
        public void Init(AbstractBattleUnit unitWeAreSelectingFor, AbstractCard card)
        {
            GameCardDisplay.GameCard.SetToAbstractCardAttributes(card);
            Card = card;
            UnitSelected = unitWeAreSelectingFor;
        }

        bool IsSelectable()
        {
            if (!Card.IsValidForClass(UnitSelected))
            {
                return false;
            }
            if (IsTaken)
            {
                return false;
            }
            return true;
        }

        // Use this for initialization
        void Start()
        {
            SelectCardButton.onClick.AddListener(() =>
            {
                if (IsSelectable())
                {
                    IsTaken = true;
                    UnitSelected.AddCardToPersistentDeck(this.Card);
                    GameState.Instance.CardInventory.Remove(this.Card);
                }
            });
        }

        // Update is called once per frame
        void Update()
        {
            if (IsSelectable())
            {
                CardNotAvailableText.gameObject.SetActive(false);
            }
            else if (IsTaken)
            {
                CardNotAvailableText.gameObject.SetActive(true);
                CardNotAvailableText.text = "SOLD";
                SelectCardButton.interactable = false;
            }
            else if (!Card.IsValidForClass(UnitSelected))
            {
                CardNotAvailableText.gameObject.SetActive(true);
                CardNotAvailableText.text = $"NOT AVAILABLE FOR {UnitSelected.SoldierClass.Name().ToUpper()}"; 
                SelectCardButton.interactable = false;
            }
        }
    }
}