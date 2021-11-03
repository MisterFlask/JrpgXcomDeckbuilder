using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Missions
{
    public class StrategicMissionReward : AbstractMissionReward
    {
        public override string GenericDescription()
        {
            return "Get a significant bonus to the final battle.";
        }

        public override void OnReward()
        {
            GameState.Instance.StrategicDetails.StrategicTempHpStacks += 20;
        }
    }
}