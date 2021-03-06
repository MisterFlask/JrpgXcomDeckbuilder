﻿using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Uncommon
{
    public class TheGreaterGood : MonoBehaviour
    {
        // cost 2
        // ALL allies gain 3 block.
        // Power: Whenever you Sacrifice, deal 10 damage to ALL enemies and draw a card.
    }


    public class TheGreaterGoodPower : AbstractStatusEffect
    {

        public TheGreaterGoodPower()
        {
            Name = "The Greater 'Good'";
        }

        public override void ProcessProc(AbstractProc proc)
        {
            if (proc is SacrificeProc)
            {
                foreach(var enemy in state().EnemyUnitsInBattle)
                {
                    action().DamageUnitNonAttack(enemy, this.OwnerUnit, 10);
                }
                action().DrawCards(1);
            }
        }

        public override string Description => "Whenever you Sacrifice, deal 10 damage to ALL enemies and draw a card.";
    }
}