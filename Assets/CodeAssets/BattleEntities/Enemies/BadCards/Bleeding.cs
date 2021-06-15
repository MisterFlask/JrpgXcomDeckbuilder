using Assets.CodeAssets.Cards;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.BadCards
{
    public class Bleeding : AbstractCard
    {
        private int Damage;

        public Bleeding(int damage = 3)
        {
            Damage = damage;
            AddSticker(new ExhaustCardSticker());
        }

        public override string DescriptionInner()
        {
            return $"Retained: Take {Damage} damage";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
        }

        public override void InHandAtEndOfTurnAction()
        {
            ActionManager.Instance.DamageUnitNonAttack(Owner, null, Damage);
        }
    }
}