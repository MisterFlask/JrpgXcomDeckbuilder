using Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.ChessCourt
{
    public class MeltingKnight : AbstractEnemyUnit
    {
        public MeltingKnight()
        {
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/Subduer");
            this.Description = "???";
            this.EnemyFaction = EnemyFaction.CHESSCOURT;
            CharacterNicknameOrEnemyName = "Melting Knight";
            this.MaxHp = 35;
            //Big attack, but has to charge up first
        }

        public override void AssignStatusEffectsOnCombatStart()
        {
            // todo
            StatusEffects.Add(new ArmoredStatusEffect()
            {
                Stacks = 2
            });

            StatusEffects.Add(new AbominationStatusEffect()
            {
                Stacks = 2
            });
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromBaseDamage.AttackRandomPc(this, 15, 1),
                IntentsFromBaseDamage.DefendSelf(this, 15),
                IntentsFromBaseDamage.BuffSelf(this, new StrengthStatusEffect(), 3);
        }

    }
}