using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

/// <summary>
/// These are used for card "passives"
/// </summary>
public abstract class AbstractCardSticker : MonoBehaviour
{
    public AbstractCard CardAttachedTo { get; set; }
    public AbstractCard card => CardAttachedTo;
    public CardStickerPrefab Prefab { get; set; }
    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon();

    public string Title_deprecated { get; set; } = "Card Sticker Title";

    public string Description_deprecated { get; set; } = "Card Sticker Description";

    /// <summary>
    /// Added onto the end of the card description.
    /// </summary>
    public virtual string CardDescriptionAddendum()
    {
        return "";
    }

    public virtual string GetCardTooltipIfAny()
    {
        return null;
    }

    public virtual void OnAddedToCardInner(AbstractCard card)
    {
        // do stuff like change its attack damage or whatever
    }

    public void OnAddedToCard(AbstractCard card)
    {
        
    }

    public AbstractCardSticker CopySticker()
    {
        return this.MemberwiseClone() as AbstractCardSticker;
    }


    public virtual void OnThisCardPlayed(AbstractBattleUnit target)
    {

    }

    public virtual void OnCardDrawn(AbstractCard card)
    {

    }
    public virtual void EndOfBattlePassiveTrigger()
    {

    }
    public virtual void OnTurnStart(AbstractCard card)
    {

    }


    public virtual void OnEndOfTurnWhileInHand(AbstractCard card)
    {
        
    }

    /// <summary>
    /// Return "false" if you want to avoid having this sticker attached to the card provided.
    /// </summary>
    public virtual bool IsCardTagApplicable(AbstractCard card)
    {
        return true;
    }

}



public class GildedCardSticker: AbstractCardSticker
{

    public GildedCardSticker(int initialValue)
    {
        this.GildedValue = initialValue;
    }
    public override string CardDescriptionAddendum()
    {
        return $"Stash {GildedValue}";
    }

    public int GildedValue { get; set; }
    public override void EndOfBattlePassiveTrigger()
    {
        if (!this.CardAttachedTo.IsExhausted())
        {
            CardAbilityProcs.ChangeMoney(GildedValue);
        }
    }

    public override void OnThisCardPlayed(AbstractBattleUnit target)
    {
        if (card.Id == CardAttachedTo.Id)
        {
            GildedValue -= 2;
            if (GildedValue < 0)
            {
                GildedValue = 2;
            }
        }
    }
}

public class ExhaustCardSticker : AbstractCardSticker
{
    public override string CardDescriptionAddendum()
    {
        return "Exhaust.";
    }

    public override void OnThisCardPlayed(AbstractBattleUnit target)
    {
        card.Action_Exhaust();
    }
}

public class BasicAttackTargetSticker: AbstractCardSticker
{
    public override string CardDescriptionAddendum()
    {
        return $"Deal {card.DisplayedDamage()} to target.";
    }

    public override void OnThisCardPlayed(AbstractBattleUnit target)
    {
        ActionManager.Instance.AttackWithCard(card, target);
    }
}

public class BasicDefendTargetSticker : AbstractCardSticker
{
    public override string CardDescriptionAddendum()
    {
        return $"Apply {card.DisplayedDefense()} block to target.";
    }

    public override void OnThisCardPlayed(AbstractBattleUnit target)
    {
        ActionManager.Instance.ApplyDefenseFromCard(card, target);
    }
}

public class BasicDefendSelfSticker : AbstractCardSticker
{
    public override string CardDescriptionAddendum()
    {
        return $"Apply {card.DisplayedDefense()} block to self.";
    }

    public override void OnThisCardPlayed(AbstractBattleUnit target)
    {
        ActionManager.Instance.ApplyDefenseFromCard(card, card.Owner);
    }
}
public class BasicApplyStatusEffectToTargetSticker : AbstractCardSticker
{

    public BasicApplyStatusEffectToTargetSticker(AbstractStatusEffect effect, int stacks)
    {
        Effect = effect;
        Stacks = stacks;
    }

    public AbstractStatusEffect Effect { get; }
    public int Stacks { get; }

    public override string CardDescriptionAddendum()
    {
        return $"Apply {Stacks} {Effect.Name} to target.";
    }

    public override void OnThisCardPlayed(AbstractBattleUnit target)
    {
        ActionManager.Instance.ApplyStatusEffect(target, Effect, Stacks);
    }
}

public class BasicApplyStatusEffectToSelfSticker : AbstractCardSticker
{

    public BasicApplyStatusEffectToSelfSticker(AbstractStatusEffect effect, int stacks)
    {
        Effect = effect;
        Stacks = stacks;
    }

    public AbstractStatusEffect Effect { get; }
    public int Stacks { get; }

    public override string CardDescriptionAddendum()
    {
        return $"Apply {Stacks} {Effect.Name} to self.";
    }

    public override void OnThisCardPlayed(AbstractBattleUnit target)
    {
        ActionManager.Instance.ApplyStatusEffect(card.Owner, Effect, Stacks);
    }
}