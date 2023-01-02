using Assets.CodeAssets.BattleEntities.Enemies.UnitSquad;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeAssets.UI_V2.RunMap
{
    public abstract class MapNodeBehavior
    {
        public static MapNodeBehavior GetRandomBehaviorForFloor(int floor)
        {
            var rand = Random.Range(0, 10);
            if (rand < 3)
            {
                return CombatNodeBehavior.GenerateForFloorAndAct(floor);
            }
            else if (rand < 6)
            {
                return HardCombatNodeBehavior.GenerateForFloorAndAct(floor);
            }
            else
            {
                return RestNodeBehavior.Generate();
            }
        }
        
        public abstract void OnEnterNode();

        public ProtoGameSprite PrimaryProtoSprite { get; set; }
        public ProtoGameSprite SecondaryProtoSprite { get; set; }
    }


    public class HardCombatNodeBehavior : MapNodeBehavior
    {
        AbstractMission Mission { get; set; }
        public static HardCombatNodeBehavior GenerateForFloorAndAct(int floor)
        {
            return new HardCombatNodeBehavior
            {
                Mission =
                    new KillEnemiesMission()
                    {
                        DaysUntilExpiration = 1000,
                        Difficulty = 1,
                        MaxNumberOfFriendlyCharacters = 3,
                        Name = AbstractMission.GenerateMissionName(),
                        Rewards = new List<AbstractMissionReward> { new GoldMissionReward(60) },
                        EnemySquad = GameAct.GetSquadForAct(1, SquadType.ELITE),
                        ProtoSprite = AbstractMission.RetrieveIconFromMissionIconFolder("cash")
                    },

                PrimaryProtoSprite = ProtoGameSprite.MapIcon("warlock-eye")
        };
        }

        public override void OnEnterNode()
        {
            GameScenes.SwitchToBattleScene(Mission, GameState.Instance.AllyUnitsSentOnRun);
        }
    }

    public class RestNodeBehavior : MapNodeBehavior
    {
        public static RestNodeBehavior Generate()
        {
            return new RestNodeBehavior
            {
                PrimaryProtoSprite = ProtoGameSprite.MapIcon("watchtower")
            };
        }

        public override void OnEnterNode()
        {
            // show rest popup; todo
        }
    }


    public class CombatNodeBehavior: MapNodeBehavior {

        public CombatNodeBehavior()
        {
            PrimaryProtoSprite = ProtoGameSprite.MapIcon("diamond-hilt");
        }
        
        AbstractMission Mission { get; set; }
        public static CombatNodeBehavior GenerateForFloorAndAct(int floor)
        {
            return new CombatNodeBehavior
            {
                Mission =
                    new KillEnemiesMission()
                    {
                        DaysUntilExpiration = 1000,
                        Difficulty = 1,
                        MaxNumberOfFriendlyCharacters = 3,
                        Name = AbstractMission.GenerateMissionName(),
                        Rewards = new List<AbstractMissionReward> { new GoldMissionReward(60) },
                        EnemySquad = GameAct.GetSquadForAct(1, SquadType.NORMAL),
                        ProtoSprite = AbstractMission.RetrieveIconFromMissionIconFolder("cash")
                    }
            };
        }

        public override void OnEnterNode()
        {
            GameScenes.SwitchToBattleScene(Mission, GameState.Instance.AllyUnitsSentOnRun);
        }
    }
}
