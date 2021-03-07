using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class Mobility : AbstractCard
{
    public Mobility()
    {
        Name = "Mobility";
        CardType = CardType.PowerCard;
        TargetType = TargetType.NO_TARGET_OR_SELF;
        Rarity = Rarity.UNCOMMON;
    }
    public override string DescriptionInner()
    {
        return $"Whenever {Owner.CharacterFullName} gains or loses Advanced, add a Flanking Shot to your hand.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
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
