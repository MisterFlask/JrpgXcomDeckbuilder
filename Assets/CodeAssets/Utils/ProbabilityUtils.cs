
using System;
using UnityEngine;

namespace Assets.CodeAssets.Utils
{
    public static class ProbabilityUtils
    {
        public static void PerformWithProbability(float probability, Action action)
        {
            var dieRolled = UnityEngine.Random.Range(0, 100);
            if (probability * 100 < dieRolled)
            {
                action();
            }
        }
    }
}