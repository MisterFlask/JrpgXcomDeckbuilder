using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Starting
{
    public class ArchonAttack : Attack
    {
        public ArchonAttack()
        {
            ProtoSprite = ProtoGameSprite.ArchonIcon("bolter-gun");
        }
    }
}