using UnityEngine;
using System.Collections;

public class Parasite : AbstractCard
{
    public Parasite()
    {
        this.Name = "Parasite";
    }

    public override string Description()
    {
        return $"Exhaust.  If not played: Take 5 damage at end of turn.";
    }

    public override void InHandAtEndOfTurnAction()
    {
        action().DamageUnitNonAttack(Owner, null, 5);
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        ActionManager.Instance.ExpendCard(this);
    }
}
