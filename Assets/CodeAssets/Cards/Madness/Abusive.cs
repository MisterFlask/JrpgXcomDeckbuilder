using UnityEngine;
using System.Collections;
using System.Linq;

public class Abusive : AbstractCard
{
    public override string Description()
    {
        return "At end of turn, deals 5 damage to your highest-health ally if in your hand.";

    }

    protected override void OnPlay(AbstractBattleUnit target)
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
