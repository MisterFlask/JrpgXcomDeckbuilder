using UnityEngine;
using System.Collections;
using System;
using System.Linq;

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

    public static DebuffOtherIntent StatusEffectToRandomPc(
        AbstractBattleUnit source,
        AbstractStatusEffect effect,
        int stacks)
    {
        return StatusEffect(source,
            GameState.Instance.AllyUnitsInBattle.Where(item => !item.IsDead).PickRandom(),
            effect, 
            stacks);

    }

    public static DebuffOtherIntent StatusEffect(
        AbstractBattleUnit source,
        AbstractBattleUnit target, 
        AbstractStatusEffect effect,
        int stacks)
    {
        return new DebuffOtherIntent(source, () =>
        {
            ActionManager.Instance.ApplyStatusEffect(target, effect, stacks);
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
