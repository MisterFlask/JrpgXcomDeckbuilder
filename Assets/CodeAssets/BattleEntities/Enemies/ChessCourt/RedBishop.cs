using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.ChessCourt
{
    public class RedBishop : AbstractEnemyUnit
    {

        public override void AssignStatusEffectsOnCombatStart()
        {
            StatusEffects.Add(new RedBishopStatusEffect()
            {
                Stacks = 30
            });
        }

        //Black Bishop/Red Bishop: All-Around Helper attack pattern for each.  
        //Black bishop:  When it deals at least 8 combat damage, gain 2 strength.  
        //Red: When it does at least 8 combat damage, ALL soldiers gains 15 stress.
        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromBaseDamage.DefendSelf(this, 5),
                IntentsFromBaseDamage.AttackRandomPc(this, 8, 2));
        }
    }

    public class RedBishopStatusEffect : AbstractStatusEffect
    {
        // when deals at least 8 combat damage, gain 2 strength
        public override string Description => "Whenever this unit deals at least 8 combat damage, it deals [stacks] stress to the unit it's damaging.";

        public override void OnStriking(AbstractBattleUnit unitStruck, AbstractCard cardUsedIfAny, int damageAfterBlockingAndModifiers)
        {
            if (damageAfterBlockingAndModifiers > 0)
            {
                ActionManager.Instance.ApplyStress(unitStruck, Stacks);
            }
        }
    }
}