using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.CogCards.Starting
{
    public class CogAttack : Shoot
    {
        public CogAttack()
        {
            ProtoSprite = ProtoGameSprite.CogIcon("laser-turret");
        }
    } 
}