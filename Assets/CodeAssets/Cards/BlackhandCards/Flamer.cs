using UnityEngine;
using System.Collections;

public class Flamer : AbstractCard
{
    public Flamer()
    {
        BaseTechValue = 5;
    }

    public override string Description()
    {
        return $"Applies {TechValue} Burning.  Increase the cost of this card by 1.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        this.EnergyCostMod += 1; //todo
    }
}
