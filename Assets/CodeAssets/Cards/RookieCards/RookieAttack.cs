using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.RookieCards
{
    public class RookieAttack : Attack
    {
        public RookieAttack()
        {
            ProtoSprite = ProtoGameSprite.RookieIcon("gunshot");
        }
    }
}