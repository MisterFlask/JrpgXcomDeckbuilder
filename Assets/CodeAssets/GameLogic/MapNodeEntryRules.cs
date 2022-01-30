using Map;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.GameLogic
{
    public class MapNodeEntryRules
    {
        public static void SetMapLockStatus(bool locked)
        {
            MapPlayerTracker.Instance.Locked = locked;
        }

        public static void EnterMapNode(MapNode entered)
        {
            var nodeType = entered.Node.nodeType;
            if (nodeType.IsCombatNode())
            {
                GameState.Instance.CurrentMission = MissionGenerator.GenerateMissionForNode(entered);
            }

            if (nodeType == NodeType.Store)
            {
                
            }

            if (nodeType  == NodeType.RestSite)
            {

            }

        }
    }
}