using UnityEngine;
using System.Collections;

public class Distracted : AbstractCard
{
    

    public Distracted()
    {
        this.Unplayable = true;
        this.Name = "Distracted";
    }

    public override string Description()
    {
        return $"Unplayable.  Exhaust at the end of your turn.";
    }

    public override void InHandAtEndOfTurnAction()
    {
        ActionManager.Instance.ExhaustCard(this);
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
    }
}
