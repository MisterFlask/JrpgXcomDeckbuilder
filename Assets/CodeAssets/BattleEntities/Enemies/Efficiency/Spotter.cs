using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.Efficiency
{
    // rotate between attack-with-debuff (adds Targeting Reticle to deck) and shielding self
    public class Spotter : AbstractBattleUnit
    {
        public Spotter()
        {
            CharacterNicknameOrEnemyName = "Spotter W-203-series";
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.RandomIntent(
                IntentsFromPercentBase.AttackRandomPcWithCardToDiscardPile(
                    new TargetingReticle(),
                    this,
                    10, 
                    1),
                IntentsFromPercentBase.DefendSelf(this, 50));
        }
    }
}