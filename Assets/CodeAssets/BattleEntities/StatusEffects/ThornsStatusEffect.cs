using UnityEngine;
using System.Collections;

public class ThornsStatusEffect : AbstractStatusEffect
{
    public ThornsStatusEffect()
    {
        Name = "Thorns";
        ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/thorny-vine", Color.green);
    }

    public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
    {
        action().DamageUnitNonAttack(unitStriking, null, Stacks);
    }

    public override string Description => "Deals damage to attackers equal to number of stacks";
}
