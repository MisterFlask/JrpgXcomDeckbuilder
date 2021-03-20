using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Effects
{
    public class EvadeStatusEffect : AbstractStatusEffect
    {
        public override string Description => "Receive 33% less damage.";

        public override float DefenseReceivedIncrementalMultiplier()
        {
            return 0.66f;
        }
    }
}