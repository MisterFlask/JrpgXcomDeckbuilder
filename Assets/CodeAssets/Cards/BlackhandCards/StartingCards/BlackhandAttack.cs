using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.BlackhandCards.StartingCards
{
    public class BlackhandAttack : Shoot
    {
        public BlackhandAttack()
        {
            ProtoSprite = ProtoGameSprite.BlackhandIcon("fire-ray");
        }
    }
}