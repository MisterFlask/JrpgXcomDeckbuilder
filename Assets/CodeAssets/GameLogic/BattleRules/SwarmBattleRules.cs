using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.GameLogic
{
    public class SwarmBattleRules
    {
        public static void RunSwarmBattleRules(AbstractCard card)
        {
            var numSwarmCardsInHand = GameState.Instance.Deck.Hand.Where(item => item.CardTags.Contains(BattleCardTags.SWARM)).Count();
            ActionManager.Instance.DamageUnitNonAttack(card.Owner, null, numSwarmCardsInHand);
        }
    }
}