using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Starting
{
    public class CogAttack : Attack
    {
        public CogAttack()
        {
            ProtoSprite = ProtoGameSprite.CogIcon("laser-turret");
        }
    } 
}