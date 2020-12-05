using UnityEngine;
using System.Collections;

public class BurningStatusEffect : AbstractStatusEffect
{
    public override string Description => $"Target takes damage per turn equal to the number of stacks of burning, then decreases Stacks by 2.  If the target has Flammable when Burning is applied, it consumes all stacks of Flammable and applies that much Burning.";

    public override void OnTurnStart()
    {
        ActionManager.Instance.AttackUnitForDamage(OwnerUnit, null, Stacks);
        Stacks -= 2;
    }

    public override void OnApplicationOrIncrease()
    {
        var stacksOfFlammable = OwnerUnit.GetStatusEffect<FlammableStatusEffect>();
        if (stacksOfFlammable != null)
        {
            var stacks = stacksOfFlammable.Stacks;
            ActionManager.Instance.RemoveStatusEffect<FlammableStatusEffect>(OwnerUnit);
            ActionManager.Instance.ApplyStatusEffect(OwnerUnit, new BurningStatusEffect(), stacks);
        }
    }
}
