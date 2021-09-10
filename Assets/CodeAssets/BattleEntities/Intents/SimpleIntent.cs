using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Assets.CodeAssets.BattleEntities.Enemies.Efficiency;

public abstract class SimpleIntent : AbstractIntent
{

    public SimpleIntent(AbstractBattleUnit source, ProtoGameSprite protoSprite): base(source)
    {
        this.ProtoSprite = protoSprite;
    }

    public override string GetOverlayText()
    {
        return "";
    }


    protected override IntentPrefab GeneratePrefab(GameObject parent)
    {
        var parentPrefab = ServiceLocator.GameObjectTemplates().AttackPrefab;
        var returnedPrefab = parentPrefab.Spawn(parent.transform);
        return returnedPrefab;
    }
}




public static class IntentsFromPercentBase
{
    public static List<AbstractIntent> AttackRandomPc(
        AbstractBattleUnit source,
        int percentDamage, int numHits = 1)
    {
        int damagePerHit = GameState.Instance.DoomCounter.GetAdjustedDamage(percentDamage);
        return SingleUnitAttackIntent.AttackRandomPc(source, damagePerHit, numHits)
            .ToSingletonList<AbstractIntent>();
    }
    public static List<AbstractIntent> AttackRandomPcWithDebuff(
        AbstractStatusEffect debuff_requiresSetStacks,
        AbstractBattleUnit source,
        int percentDamage,
        int numHits = 1)
    {
        int damagePerHit = GameState.Instance.DoomCounter.GetAdjustedDamage(percentDamage);

        var attackIntent = SingleUnitAttackIntent.AttackRandomPc(source, damagePerHit, numHits);
        return new List<AbstractIntent>
        {
            attackIntent,
            DebuffOtherIntent.StatusEffect(source, attackIntent.Target, debuff_requiresSetStacks.CloneStatusEffect())
        };
    }

    public static List<AbstractIntent> TargetRandomNpcWithMagic(AbstractBattleUnit source, 
        Action action)
    {
        return new List<AbstractIntent>();
    }


    public static List<AbstractIntent> AttackRandomPcWithCardToDiscardPile(
        AbstractCard card,
        AbstractBattleUnit source,
        int percentDamage,
        int numHits = 1)
    {
        int damagePerHit = GameState.Instance.DoomCounter.GetAdjustedDamage(percentDamage);

        var attackIntent = SingleUnitAttackIntent.AttackRandomPc(source, damagePerHit, numHits);
        return new List<AbstractIntent>
        {
            attackIntent,
            DebuffOtherIntent.AddCardToDiscardPile(source, attackIntent.Target, card)
        };
    }

    public static List<AbstractIntent> AttackSetOfPcs(AbstractBattleUnit source,
        int percentDamage, int numHits, int numEnemies)
    {
        int damagePerHit = GameState.Instance.DoomCounter.GetAdjustedDamage(percentDamage);

        var enemiesToHit = GameState.Instance.AllyUnitsInBattle
            .Where(item => !item.IsDead)
            .PickRandom(numEnemies);
        return enemiesToHit
            .Select(target => new SingleUnitAttackIntent(source, target, damagePerHit, numHits))
            .ToList<AbstractIntent>();
    }

    public static List<AbstractIntent> AttackAllPcs(AbstractBattleUnit source,
        int percentDamage, int numHits)
    {
        int damagePerHit = GameState.Instance.DoomCounter.GetAdjustedDamage(percentDamage);
        return AttackSetOfPcs(source, damagePerHit, numHits, 20); //20 arbitrarily chosen; will hit everyone
    }

    public static List<AbstractIntent> BuffSelf(AbstractBattleUnit self,
        AbstractStatusEffect statusEffect,
        int stacks = 1)
    {
        return new BuffSelfIntent(self, statusEffect, stacks)
            .ToSingletonList<AbstractIntent>();
    }
    public static List<AbstractIntent> DefendSelf(AbstractBattleUnit self,
        int shieldPercent)
    {
        return new DefendSelfIntent(self, 
            GameState.Instance.DoomCounter.GetAdjustedDamage(shieldPercent))
            .ToSingletonList<AbstractIntent>();
    }

    public static List<AbstractIntent> Charging(AbstractBattleUnit unit)
    {
        return new List<AbstractIntent>();
    }
}
public static class IntentRotation
{
    public static List<AbstractIntent> RandomIntent(params List<AbstractIntent>[] possibilities)
    {
        return possibilities.ToList().PickRandom();
    }

    public static List<AbstractIntent> FixedRotation(params List<AbstractIntent>[] possibilities)
    {
        var turnModNumOptions = GameState.Instance.BattleTurn % possibilities.Count();
        return possibilities.ToList()[turnModNumOptions];
    }

    public static List<AbstractIntent> LeadupAndRepeatLastOneForever(
        params List<AbstractIntent>[] leadup)
    {
        var turnOrLast = Math.Min(GameState.Instance.BattleTurn, leadup.Count() - 1);
        return leadup[turnOrLast];
    }

    public static List<AbstractIntent> LeadupAndThenGenerateIntentsFromFunction(
        Func<int, List<AbstractIntent>> generateLastAction,
        params List<AbstractIntent>[] leadup)
    {
        var turn = GameState.Instance.BattleTurn;

        if (turn <= leadup.Count() - 1)
        {
            return leadup[turn];
        }

        return generateLastAction(GameState.Instance.BattleTurn);
    }

}

#region obsolete
[Obsolete]
public static class IntentsFromBaseDamage
{
    [Obsolete]
    public static List<AbstractIntent> AttackRandomPc(
        AbstractBattleUnit source, 
        int damagePerHit, int numHits = 1)
    {
        return SingleUnitAttackIntent.AttackRandomPc(source, damagePerHit, numHits)
            .ToSingletonList<AbstractIntent>();
    }

    public static List<AbstractIntent> AttackSetOfPcs(AbstractBattleUnit source,
        int damagePerHit, int numHits, int numEnemies)
    {
        var enemiesToHit = GameState.Instance.AllyUnitsInBattle
            .Where(item => !item.IsDead)
            .PickRandom(numEnemies);
        return enemiesToHit
            .Select(target => new SingleUnitAttackIntent(source, target, damagePerHit, numHits))
            .ToList<AbstractIntent>();
    }

    public static List<AbstractIntent> AttackAllPcs(AbstractBattleUnit source,
        int damagePerHit, int numHits)
    {
        return AttackSetOfPcs(source, damagePerHit, numHits, 20); //20 arbitrarily chosen; will hit everyone
    }

    public static List<AbstractIntent> BuffSelf(AbstractBattleUnit self,
        AbstractStatusEffect statusEffect,
        int stacks = 1)
    {
        return new BuffSelfIntent(self, statusEffect, stacks)
            .ToSingletonList<AbstractIntent>();
    }


    public static List<AbstractIntent> RandomIntent(params List<AbstractIntent>[] possibilities)
    {
        return possibilities.ToList().PickRandom();
    }

    public static List<AbstractIntent> FixedRotation(params List<AbstractIntent>[] possibilities)
    {
        var turnModNumOptions = GameState.Instance.BattleTurn % possibilities.Count();
        return possibilities.ToList()[turnModNumOptions];
    }

    public static List<AbstractIntent> LeadupAndRepeatLastOneForever(
        params List<AbstractIntent>[] leadup)
    {
        var turnOrLast = Math.Min(GameState.Instance.BattleTurn, leadup.Count() - 1);
        return leadup[turnOrLast];
    }

    public static List<AbstractIntent> LeadupAndThenGenerateIntentsFromFunction(
        Func<int, List<AbstractIntent>> generateLastAction,
        params List<AbstractIntent>[] leadup)
    {
        var turn = GameState.Instance.BattleTurn;

        if (turn <= leadup.Count() - 1)
        {
            return leadup[turn];
        }

        return generateLastAction(GameState.Instance.BattleTurn);
    }
}
#endregion
