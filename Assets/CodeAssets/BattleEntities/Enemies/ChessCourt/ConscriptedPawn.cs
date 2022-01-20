using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.ChessCourt
{
    public class ConscriptedPawn : AbstractEnemyUnit
    {
        public ConscriptedPawn()
        {
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/Subduer");
            this.Description = "???";
            this.EnemyFaction = EnemyFaction.CHESSCOURT;
            CharacterNicknameOrEnemyName = "Conscripted Pawn";
            this.MaxHp = 11;
            //Big attack, but has to charge up first
        }

        public override void AssignStatusEffectsOnCombatStart()
        {
            // todo
            // StatusEffects.Add(new FlankedStatusEffect());
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromBaseDamage.AttackRandomPc(this, 4,1),
                IntentsFromBaseDamage.DefendSelf(this, 5));
        }

    }
}