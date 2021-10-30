using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.RookieCards
{
    public class RookieAttack : Shoot
    {
        public RookieAttack()
        {
            ProtoSprite = ProtoGameSprite.RookieIcon("gunshot");
        }
    }
}