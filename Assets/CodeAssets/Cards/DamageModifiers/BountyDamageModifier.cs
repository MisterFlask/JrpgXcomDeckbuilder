using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DamageModifiers
{
    public  static class BountyDamageModifier
    {
        public static LethalTriggerDamageModifier Create()
        {
            return new LethalTriggerDamageModifier("Bounty", (unitKilled) =>
            {
                if (unitKilled.IsBoss)
                {
                    GameState.Instance.Credits += 30;
                }
                else if (unitKilled.IsElite)
                {
                    GameState.Instance.Credits += 15;
                }
            });
        }
    }
}