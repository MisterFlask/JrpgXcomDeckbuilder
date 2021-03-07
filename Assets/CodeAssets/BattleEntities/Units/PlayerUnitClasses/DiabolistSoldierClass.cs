using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses
{
    public class DiabolistSoldierClass : AbstractSoldierClass
    {
        public override string Name()
        {

            return "Diabolist";
        }

        public override List<AbstractCard> StartingCards()
        {
            return new List<AbstractCard>()
            {
                new Attack(),
                new Attack(),
                new Attack(),
                new Attack()

            };
        }
    }
}