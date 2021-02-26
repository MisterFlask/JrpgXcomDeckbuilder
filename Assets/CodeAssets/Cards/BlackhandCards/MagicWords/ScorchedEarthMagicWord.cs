using Assets.CodeAssets.GameLogic;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.MagicWords
{
    public class ScorchedEarthMagicWord : MagicWord
    {
        public override string MagicWordDescription => "Whenever you exhaust a card while this is in your hand, increase its damage by X this combat.";

        public override string MagicWordTitle => "Scorched Earth";
    }
}