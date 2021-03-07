using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

/// <summary>
/// Whenever you take damage, exhaust a random card in your discard or draw piles, and gain 1 Strength.
/// </summary>
public class Rage : AbstractCard
{
    public Rage()
    {
        SetCommonCardAttributes("Rage", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1);
    }

    public override string DescriptionInner()
    {
        return $"Gain 1 Power whenever you take damage, then exhaust a card from your discard pile.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().ApplyStatusEffect(Owner, new PowerStatusEffect(), 1);
        var cardToExpend = state().Deck.DiscardPile.PickRandom();
        action().ExhaustCard(cardToExpend);
    }
}
