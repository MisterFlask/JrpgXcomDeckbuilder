using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Intents
{
    public class SummonEnemiesOrElseHealIntent : AbstractIntent
    {

        public AbstractBattleUnit MinionToCreate;
        public SummonEnemiesOrElseHealIntent(AbstractBattleUnit source, 
            AbstractBattleUnit minionToCreate,
            ProtoGameSprite protoSprite = null) : base(source, null, protoSprite)
        {
            this.MinionToCreate = minionToCreate;
        }

        public override string GetGenericDescription()
        {
            return "This enemy intends to summon allies next round.  If there aren't spots available, will heal 1/3 of its HP.";
        }

        public override string GetOverlayText()
        {
            return "";
        }

        protected override void Execute()
        {
            if (CurrentlyAvailableForUsage())
            {
                ActionManager.Instance.CreateEnemyMinionInBattle(MinionToCreate);
            }
            else 
            {
                ActionManager.Instance.HealUnit(Source, Source.MaxHp / 3);
            }
        }

        protected override bool CurrentlyAvailableForUsage()
        {
            return BattleScreenPrefab.INSTANCE.IsRoomForAnotherEnemy();
        }

        protected override IntentPrefab GeneratePrefab(GameObject parent)
        {
            throw new System.NotImplementedException();
        }
    }
}