using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    // Whenever ANY enemy in the combat dies, gains +3 strength.
    public class SoulSucker : AbstractStatusEffect
    {
        public override string Description => "Whenever a unit dies in combat, gain [stacks] strength.";

        public override void ProcessProc(AbstractProc proc)
        {
            if (proc is CharacterDeathProc)
            {
                ActionManager.Instance.ApplyStatusEffect(OwnerUnit, new StrengthStatusEffect(), Stacks);
            }
        }
    }
}