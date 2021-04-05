using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards.Rare
{
    public class Impenetrable : AbstractCard
    {
        // Gain 15 block.  Taunt an enemy.  It gains Weak this turn.

        // Brute: Taunt ALL enemies.


        public Impenetrable()
        {
            SetCommonCardAttributes("Impenetrable", Rarity.RARE, TargetType.ENEMY, CardType.SkillCard, 2);
            BaseDefenseValue = 15;
            Stickers.Add(new BasicDefendSelfSticker());
        }

        public override string DescriptionInner()
        {
            return "Taunt target enemy.  It gains Weak this turn.  Brute: Taunt ALL enemies.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyDefenseToTarget(this.Owner);
            action().TauntEnemy(target, this.Owner);
            CardAbilityProcs.Brute(this, () =>
            {
                foreach(var enemy in state().EnemyUnitsInBattle)
                {
                    action().TauntEnemy(enemy, this.Owner);
                }
            });
        }

    }
}