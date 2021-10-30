using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.Efficiency
{
    // rotate between attack-with-debuff (adds Targeting Reticle to deck) and shielding self
    public class EfficiencySpotter : AbstractEnemyUnit
    {
        public EfficiencySpotter()
        {
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/Spotter");
            this.Description = "???";
            this.EnemyFaction = EnemyFaction.EFFICIENCY;
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