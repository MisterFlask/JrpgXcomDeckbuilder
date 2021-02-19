using UnityEngine;
using System.Collections;
using System.Linq;

public class Bayonet : AbstractCard
{
    public Bayonet()
    {
        SoldierClassCardPools.Add(typeof(RookieClass));
        Name = "Bayonet";
        BaseDamage = 10;
        TargetType = TargetType.ENEMY;
        CardType = CardType.AttackCard;
        this.ProtoSprite = GameIconProtoSprite.FromGameIcon(path: "Sprites/bayonet");

    }

    public override int BaseEnergyCost()
    {
        if (Owner == null)
        {
            return 2;
        }

        if (Owner.HasStatusEffect<AdvancedStatusEffect>())
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        Require.NotNull(target);
        action().AttackUnitForDamage(target, Owner, BaseDamage);
        var cardDiscarded = action().PromptDiscardOfSingleCard();
        action().DoAThing(() =>
        {
            Debug.Log("Cards discarded: " + cardDiscarded.CardsSelected.GetCardNames());
        });
    }

    public override string Description()
    {
        return $"Deals {displayedDamage()} damage to an enemy unit.  Costs 1 less if Advanced.  Discard a card.";
    }
}
