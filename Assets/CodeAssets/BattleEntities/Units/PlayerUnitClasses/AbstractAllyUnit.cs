using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractAllyUnit : AbstractBattleUnit
{
    public AbstractAllyUnit()
    {
        this.IsAlly = true;
        this.IsAiControlled = false;
    }

    public override List<Intent> GetNextIntents()
    {
        return new List<Intent>();
    }

    public abstract List<AbstractCard> CardsSelectableOnLevelUp();
}
