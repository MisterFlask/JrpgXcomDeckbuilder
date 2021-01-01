using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

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


public static class Intents
{
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

    public static List<AbstractIntent> BuffSelfOrHeal(AbstractBattleUnit self,
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
