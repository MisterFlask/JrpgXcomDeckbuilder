using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.UI.Screens.BattleScreen
{
    public abstract class SelectCardInHandBehavior
    {
        protected List<AbstractCard> CardsSelected 
            => CardAnimationManager.INSTANCE.cardsInHand.Where(item => item.IsSelected).Select(item => item.LogicalCard).ToList();

        public abstract void SubmitCards(List<AbstractCard> cardsSelected);

        public string SelectCardsDisplayText = "Select Cards";

        public abstract bool IsAcceptableToSubmit();


    }


    public class DiscardCardsBehavior: SelectCardInHandBehavior
    {
        public DiscardCardsBehavior()
        {
            SelectCardsDisplayText = "Select one card to discard.";
        }

        public int MinCardsToSelect { get; set; } = 1;
        public int MaxCardsToSelect { get; set; } = 1;

        public override bool IsAcceptableToSubmit()
        {
            return CardsSelected.Count >= MinCardsToSelect && CardsSelected.Count <= MaxCardsToSelect;
        }

        public override void SubmitCards(List<AbstractCard> cardsSelected)
        {
            foreach(var card in cardsSelected)
            {
                ActionManager.Instance.DiscardCard(card);
            }
        }
    }

}