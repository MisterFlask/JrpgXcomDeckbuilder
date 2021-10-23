using UnityEngine;
using System.Collections;
using System;

public class DebuffOtherIntent : SimpleIntent
{
    public DebuffOtherIntent(AbstractBattleUnit owner, Action onPerformance): base(owner, 
        IntentIcons.DebuffIntent)
    {
    }

    public static DebuffOtherIntent SomeAction(AbstractBattleUnit owner,
        Action onPerformance)
    {
        return new DebuffOtherIntent(owner, onPerformance);
    }

    public static DebuffOtherIntent StatusEffect(
        AbstractBattleUnit source,
        AbstractBattleUnit target, 
        AbstractStatusEffect effect)
    {
        return new DebuffOtherIntent(source, () =>
        {
            ActionManager.Instance.ApplyStatusEffect(target, effect, effect.Stacks);
        });
    }

    public static DebuffOtherIntent AddCardToDiscardPile(
        AbstractBattleUnit source,
        AbstractBattleUnit target,
        AbstractCard card)
    {
        return new DebuffOtherIntent(source, () =>
        {
            var cardCopy = card.CopyCard();
            cardCopy.Owner = target;
            ActionManager.Instance.CreateCardToBattleDeckDiscardPile(cardCopy);
        });
    }

    public override string GetGenericDescription()
    {
        return "This enemy is about to apply a debuff to one or more of your soldiers.";
    }

    public override string GetOverlayText()
    {
        return ""; // no overlay text for this class of intent.
    }

    protected override void Execute()
    {
        
    }
}
