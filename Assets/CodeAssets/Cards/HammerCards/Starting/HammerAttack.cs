using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Starting
{
    public class HammerAttack : Shoot
    {
        public HammerAttack()
        {
            ProtoSprite = ProtoGameSprite.HammerIcon("thor-hammer");
        }
    }
}