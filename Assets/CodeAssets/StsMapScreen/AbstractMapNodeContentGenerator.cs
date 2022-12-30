using Assets.CodeAssets.BattleEntities.Enemies;
using Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Assets.CodeAssets.StsMapScreen
{

    public class AbstractMapNodeContentGenerator
    {
        public static List<AbstractRichMapNode> PossibleNodes = new List<AbstractRichMapNode>
        {
            new ShopRichMapNode(),
            new RoadBattleRichMapNode(),
            new BossBattleRichMapNode(),
            new StrongholdBattleRichMapNode()
        };

        public static AbstractRichMapNode GenerateNonBossNode()
        {
            var node = PossibleNodes.PickRandomWhere(item => !(item is BossBattleRichMapNode)).CloneNode();
            node.Init();
            return node;
        }

        public static AbstractRichMapNode GenerateBossNode()
        {
            var node = PossibleNodes.PickRandomWhere(item => item is BossBattleRichMapNode).CloneNode();
            node.Init();
            return node;
        }
    }
    public class ShopRichMapNode : AbstractRichMapNode
    {
        public override ProtoGameSprite GetProtoSprite()
        {
            return ProtoGameSprite.Default;
        }

        public override NodeType nodeType()
        {
            return NodeType.Store;
        }
    }

    public class RoadBattleRichMapNode : AbstractBattleRichMapNode
    {
        public override NodeType nodeType()
        {
            return NodeType.MinorEnemy;
        }
    }

    public class StrongholdBattleRichMapNode : AbstractBattleRichMapNode
    {
        public override NodeType nodeType()
        {
            return NodeType.EliteEnemy;
        }
    }

    public class BossBattleRichMapNode : AbstractBattleRichMapNode
    {
        public override NodeType nodeType()
        {
            return NodeType.Boss;
        }
    }

    public abstract class AbstractBattleRichMapNode : AbstractRichMapNode
    {
        public override ProtoGameSprite GetProtoSprite()
        {
            return ProtoGameSprite.Default; //todo
        }

        public AbstractMission Mission { get; set; } 
        public EnemyFaction Faction { get; set; }

        public override void Init()
        {
            Mission =
                new KillEnemiesMission()
                {
                    DaysUntilExpiration = 1000,
                    Difficulty = 1,
                    MaxNumberOfFriendlyCharacters = 3,
                    Name = AbstractMission.GenerateMissionName(),
                    Rewards = new List<AbstractMissionReward> { new GoldMissionReward(60) },
                    EnemySquad = MissionRules.GetRandomSquadForCurrentActAndDay(SquadType.NORMAL),
                    ProtoSprite = AbstractMission.RetrieveIconFromMissionIconFolder("cash"),
                    MissionModifiers = MissionGenerator.GetRandomMissionModifiers()
                };
        }

        public override List<ProtoGameSprite> GetSecondaryProtoSpriteIfApplicable()
        {
            return new List<ProtoGameSprite>();
        }
    }

    public abstract class AbstractRichMapNode
    {
        public AbstractRichMapNode CloneNode()
        {
            return this.MemberwiseClone() as AbstractRichMapNode;
        }

        public abstract NodeType nodeType();

        public abstract ProtoGameSprite GetProtoSprite();
        public virtual List<ProtoGameSprite> GetSecondaryProtoSpriteIfApplicable()
        {
            return null;
        }

        public virtual string GetTooltipText()
        {
            return "";
        }

        public virtual void Init()
        {

        }

        public virtual AbstractMission? GenerateMissionOrNull()
        {
            return null;
        }

        public NodeBlueprint GenerateBlueprint()
        {
            return new NodeBlueprint
            {
                nodeType = nodeType(),
                name = nodeType().ToString(),
                sprite = GetProtoSprite().ToSprite()
            };
        }
    }
}
