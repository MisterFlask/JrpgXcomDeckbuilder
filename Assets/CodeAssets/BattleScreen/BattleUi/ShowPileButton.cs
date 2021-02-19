using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeAssets.BattleScreen.BattleUi
{

    public class ShowPileButton : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI Text;
        public Button Button;
        public PileType PileType;
        // Use this for initialization
        void Start()
        {
            Button.onClick.AddListener(() =>
            {
                if (PileType == PileType.DISCARD)
                {
                    ShowDeckScreen.ShowDiscardPile();
                }else if (PileType == PileType.DRAW)
                {
                    ShowDeckScreen.ShowDrawPile();
                }else if (PileType == PileType.EXHAUST)
                {
                    ShowDeckScreen.ShowExhaustPile();
                }
            });
        }

        // Update is called once per frame
        void Update()
        {
            var pile = GetPile(PileType);

            var text = "Other Pile";
            if (PileType == PileType.DISCARD)
            {
                text = "Discard";
            }
            if (PileType == PileType.DRAW)
            {
                text = "Draw";
            }
            if (PileType == PileType.EXHAUST)
            {
                text = "Exhaust";   
            }

            Text.text = $"{text}\n{pile.Count}";
            
        }

        List<AbstractCard> GetPile(PileType type)
        {
            var state = GameState.Instance;
            if (type == PileType.DISCARD)
            {
                return state.Deck.DiscardPile;
            }
            else if (type == PileType.DRAW)
            {
                return state.Deck.DrawPile;
            }
            else
            {
                return state.Deck.ExhaustPile;
            }
        }
    }
}

public enum PileType
{
    DRAW,
    DISCARD,
    EXHAUST
}