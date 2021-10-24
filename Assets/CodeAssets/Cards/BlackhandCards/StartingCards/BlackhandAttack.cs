using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.StartingCards
{
    public class BlackhandAttack : Attack
    {
        public BlackhandAttack()
        {
            ProtoSprite = ProtoGameSprite.BlackhandIcon("fire-ray");
        }
    }
}