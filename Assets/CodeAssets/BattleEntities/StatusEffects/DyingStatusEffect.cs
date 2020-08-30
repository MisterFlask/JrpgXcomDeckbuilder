using UnityEngine;
using System.Collections;

public class DyingStatusEffect : AbstractBattleUnitAttribute
{
    public DyingStatusEffect()
    {
        this.Stacks = 3;
    }

    public override void OnTurnStart()
    {
        if (this.Stacks == 0)
        {
            action().KillUnit(OwnerUnit);
        }
        action().ApplyStatusEffect(OwnerUnit, new DyingStatusEffect(), -1);
    }
}
