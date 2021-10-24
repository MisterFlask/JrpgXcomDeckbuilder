using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Starting
{
    public class DiabolistAttack : Attack
    {
        public DiabolistAttack()
        {
            ProtoSprite = ProtoGameSprite.DiabolistIcon("gunshot");
        }
    }
}