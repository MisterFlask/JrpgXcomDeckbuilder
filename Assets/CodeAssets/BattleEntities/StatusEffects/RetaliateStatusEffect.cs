using UnityEngine;
using System.Collections;

public class RetaliateStatusEffect : AbstractStatusEffect
{
    public RetaliateStatusEffect()
    {
        Name = "Retaliate";
        ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/thorny-vine", Color.red);
    }

    public override void OnStruck(AbstractBattleUnit unitStriking, int totalDamageTaken)
    {
        if (Stacks <= 0) return;
        var damageToReturn = BattleRules.GetAnticipatedDamageToUnit(OwnerUnit, unitStriking, 5, true, null);
        action().DamageUnitNonAttack(unitStriking, null, damageToReturn);
        Stacks--;
    }

    public override string Description => "When this character is attacked, tick down stacks by 1 and deal a flat 5 damage to attackers." +
        "Scales with damage modifiers.";
}
