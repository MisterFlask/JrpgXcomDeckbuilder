using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Rare
{
    public class BackToThePit : AbstractCard
    {

        public BackToThePit()
        {
            this.SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            this.SetCommonCardAttributes("Back To The Pit", Rarity.RARE, TargetType.ENEMY, CardType.AttackCard, 3);

            this.DamageModifiers.Add(new LethalTriggerDamageModifier("Relieve 10 stress for ALL allies.", (killedUnit) =>
            {
                foreach(var ally in state().AllyUnitsInBattle)
                {
                    action().ApplyStress(ally, -10);
                }
            }));

            BaseDamage = 30;

            this.DamageModifiers.Add(new SlayerDamageModifier());
        }

        public override string DescriptionInner()
        {
            return $"Deal {BaseDamage} damage. Draw 3 cards. Lethal: Relieve 4 Stress for ALL allies.  " +
                $"Slayer.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(this.Owner, new StressStatusEffect(), -3);
            this.Action_Exhaust();
        }
    }
    
}