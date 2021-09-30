using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.Efficiency
{
    public class EfficiencySubduer : AbstractEnemyUnit
    {
        public EfficiencySubduer()
        {
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/RoboVAK", color: Color.green);
            this.Description = "???";
            this.EnemyFaction = EnemyFaction.EFFICIENCY;
            CharacterNicknameOrEnemyName = "Subduer XLR";
            //Big attack, but has to charge up first
        }

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