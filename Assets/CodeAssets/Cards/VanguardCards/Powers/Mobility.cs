﻿using UnityEngine;
using System.Collections;

public class Mobility : AbstractCard
{
    public Mobility()
    {
        Name = "Mobility";
        CardType = CardType.PowerCard;
        TargetType = TargetType.NO_TARGET_OR_SELF;
        Rarity = Rarity.UNCOMMON;
    }
    public override string Description()
    {
        return $"Whenever {Owner.CharacterFullName} gains or loses Advanced, add a Flanking Shot to your hand.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().ApplyStatusEffect(Owner, new MobilityStatusEffect(), 1);
    }
}

public class MobilityStatusEffect : AbstractStatusEffect
{
    public override string Description => $"Whenever {OwnerUnit.CharacterFullName} gains or loses Advanced, add a Flanking Shot to your hand.";

    public override int OnAnyStatusEffectApplicationToOwner(AbstractStatusEffect statusEffectApplied, int stacksAppliedOrDecremented)
    {
        if (statusEffectApplied.GetType() == typeof(AdvancedStatusEffect))
        {
            action().AddCardToHand(new FlankingShot());
        }
        return stacksAppliedOrDecremented;
    }
}
