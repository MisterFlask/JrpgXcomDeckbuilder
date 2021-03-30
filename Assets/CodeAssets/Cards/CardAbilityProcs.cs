using Assets.CodeAssets.BattleEntities.StatusEffects;
using Assets.CodeAssets.Cards.BlackhandCards.Powers;
using Assets.CodeAssets.Cards.HammerCards.Common;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.Cards
{
    /// <summary>
    /// This is just a set of helper methods for very specific card abilities-- leadership, sly, that sort of thing.
    /// </summary>
    public static class CardAbilityProcs
    {
        private static GameState state => GameState.Instance;
        private static ActionManager action => ActionManager.Instance;

        public static void Leadership(this AbstractCard card, Action thingToDo)
        {
            if (state.AllyUnitsInBattle.TrueForAll((ally) => ally == card.Owner || ally.CurrentLevel < card.Owner.CurrentLevel))
            {
                thingToDo();
            }
        }

        public static void GainEnergy(AbstractCard card, int amount = 1)
        {
            ActionManager.Instance.DoAThing(() =>
            {
                GameState.Instance.energy += amount;
            });
        }

        public static void Refund(AbstractCard card, int amount = 1)
        {
            ActionManager.Instance.DoAThing(() =>
            {
                GameState.Instance.energy+=amount;
            });
        }

        public static void Sly(this AbstractCard card, Action thingToDo)
        {
            if (state.Deck.Hand.Count < 3)
            {
                thingToDo();
                BattleRules.ProcessProc(new SlyProc { TriggeringCardIfAny = card });

            }
        }

        // If ANY ally is Empowered, decrement it by 1 and activate this effect.
        public static void Energized(AbstractCard abstractCard, Action action)
        {
            var characterWithEmpowered =  GameState.Instance.AllyUnitsInBattle.First(item => item.HasStatusEffect<EmpoweredStatusEffect>());
            ActionManager.Instance.ApplyStatusEffect(characterWithEmpowered, new EmpoweredStatusEffect(), -1);
            action();
        }

        public static void Liquidate(this AbstractCard card, Action thingToDo)
        {
            var firstRareCard = state.Deck.Hand.FirstOrDefault(item => item.Rarity == Rarity.RARE && item != card);
            if (firstRareCard != null)
            {
                thingToDo(); 

                action.ExhaustCard(firstRareCard); 
                BattleRules.ProcessProc(new LiquidateProc { TriggeringCardIfAny = card });

            }
        }

        public static void ExhaustAsAction(this AbstractCard card)
        {
            action.ExhaustCard(card);
        }

        public static bool IsVigil(this AbstractCard card)
        {
            var firstVigilCard = state.Deck.Hand.FirstOrDefault(item => item.CardTags.Contains(BattleCardTags.VIGIL));
            if (firstVigilCard == card)
            {
                return true;
            }
            return false;
        }

        public static void Brute(this AbstractCard card, Action thingToDo)
        {
            var cost3Card = state.Deck.Hand.FirstOrDefault(card => card.BaseEnergyCost() == 3);
            if (cost3Card != null)
            {
                thingToDo();
                BattleRules.ProcessProc(new BruteProc { TriggeringCardIfAny = card });

            }
        } 

        public static void Inferno(this AbstractCard card, Action thingToDo)
        {
            var burningEnemy = state.EnemyUnitsInBattle.Any(enemy => !enemy.IsDead && enemy.HasStatusEffect<BurningStatusEffect>());
            {
                thingToDo();
                BattleRules.ProcessProc(new InfernoProc { TriggeringCardIfAny = card });
            }
        }

        public static void Sacrifice(this AbstractCard card, Action thingToDo)
        {
            var sacrificableCard = state.Deck.Hand.FirstOrDefault(item => item != card);
            if (sacrificableCard != null)
            {
                sacrificableCard.ExhaustAsAction();
                action.ApplyStress(sacrificableCard.Owner, 6);
                BattleRules.ProcessProc(new SacrificeProc { TriggeringCardIfAny = card });
                thingToDo();
            }
        }

        public static void Ambush(this AbstractCard card, Action thingToDo)
        {
            if (GameState.Instance.BattleTurn <= 3)
            {
                thingToDo();
            }
        }
    }


    public class SacrificeProc: AbstractProc
    {

    }

    public class InfernoProc: AbstractProc
    {

    }

    public class BruteProc: AbstractProc
    {

    }

    public class LiquidateProc: AbstractProc
    {

    }

    public class SlyProc: AbstractProc
    {

    }

}