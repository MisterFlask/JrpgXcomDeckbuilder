using UnityEngine;
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
        return $"Whenever {Owner.CharacterName} gains or loses Advanced, add a Flanking Shot to your hand.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().ApplyStatusEffect(Owner, new MobilityStatusEffect(), 1);
    }
}

public class MobilityStatusEffect : AbstractStatusEffect
{
    public override string Description => $"Whenever {OwnerUnit.CharacterName} gains or loses Advanced, add a Flanking Shot to your hand.";

    public override void OnAnyStatusEffectApplicationToOwner(StatusEffectChange increaseOrDecrease, AbstractStatusEffect statusEffectApplied)
    {
        if (statusEffectApplied.GetType() == typeof(AdvancedStatusEffect))
        {
            action().AddCardToHand(new FlankingShot());
        }
    }
}
