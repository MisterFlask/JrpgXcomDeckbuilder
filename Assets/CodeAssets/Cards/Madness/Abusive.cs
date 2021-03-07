using UnityEngine;
using System.Collections;
using System.Linq;
using Assets.CodeAssets.Cards;

public class Abusive : AbstractCard
{
    public override string DescriptionInner()
    {
        return "At end of turn, deals 3 damage to your highest-health ally.  Leader: Deal 10 instead.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        
    }

    public override void InHandAtEndOfTurnAction()
    {
        var allies = state().AllyUnitsInBattle.Where(item => item != this.Owner);
        if (allies.Count() > 0)
        {
            action().DamageUnitNonAttack(allies.PickRandom(), null, 5);
        }

    }
}
