using UnityEngine;
using System.Collections;

/// <summary>
/// Whenever you take damage, exhaust a random card in your discard or draw piles, and gain 1 Strength.
/// </summary>
public class Rage : AbstractCard
{
    Rage()
    {
        SetCommonCardAttributes("Rage", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1);
    }

    public override string Description()
    {
        return $"Gain 1 Power whenever you take damage, then exhaust a card from your discard pile.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().ApplyStatusEffect(Owner, new PowerStatusEffect(), 1);
        var cardToExpend = state().Deck.DiscardPile.PickRandom();
        action().ExhaustCard(cardToExpend);
    }
}
