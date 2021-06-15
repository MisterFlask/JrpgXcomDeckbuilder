using Assets.CodeAssets.Cards;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Enemies.BadCards
{
    public class Winded : AbstractCard
    {
        public Winded()
        {
            /// bog-standard unplayable
        }

        public override string DescriptionInner()
        {
            return "Unplayable";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
        }
    }
}