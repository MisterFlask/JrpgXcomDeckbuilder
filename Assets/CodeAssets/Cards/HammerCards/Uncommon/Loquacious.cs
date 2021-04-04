using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.HammerCards.Uncommon
{
    public class Loquacious : AbstractCard
    {
        // Whenever you Taunt an enemy, apply 1 Weak and 1 Vulnerable.
        public override string DescriptionInner()
        {
            return "Whenever you taunt an enemy, apply 1 Weak and 1 Vulnerable.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            
        }
    }

    public class LoquaciousStatusEffect: AbstractStatusEffect
    {
        public LoquaciousStatusEffect()
        {
            this.Name = "Loquacious";
        }

        public override string Description => $"Whenever you taunt an enemy, apply {DisplayedStacks()} weakened and vulnerable.";

        public override void ProcessProc(AbstractProc proc)
        {
            if (proc is TauntProc)
            {
                var tauntproc = proc as TauntProc;
                var tauntee = tauntproc.Tauntee;

                action().ApplyStatusEffect(tauntee, new VulnerableStatusEffect(), Stacks);
                action().ApplyStatusEffect(tauntee, new WeakenedStatusEffect(), Stacks);
            }
        }
    }
}