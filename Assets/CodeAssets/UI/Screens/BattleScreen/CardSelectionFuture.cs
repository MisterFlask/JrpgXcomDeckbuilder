using HyperCard;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.UI.Screens.BattleScreen
{
    public class CardSelectionFuture
    {
        public bool IsReady => CardsSelected != null;
        public List<AbstractCard> CardsSelected { get; set; }
    }
}