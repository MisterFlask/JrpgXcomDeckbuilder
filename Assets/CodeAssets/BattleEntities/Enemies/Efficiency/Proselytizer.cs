using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.Efficiency
{
    public class Proselytizer : AbstractBattleUnit
    {
        //Increase stress of all characters by 10 => attack for 20%

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromPercentBase.Charging(this),
                IntentsFromPercentBase.Charging(this),
                IntentsFromPercentBase.AttackRandomPcWithCardToDiscardPile(
                    new TargetingReticle(),
                    this,
                    200,
                    1),
                IntentsFromPercentBase.DefendSelf(this, 50));
        }
    }
}