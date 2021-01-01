using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            .SelectMany(item => AttackRandomPc(source, damagePerHit, numHits))
            .ToList();
    }

    public static List<AbstractIntent> BuffSelfOrHeal(AbstractBattleUnit self,
        AbstractStatusEffect statusEffect,
        int stacks = 1)
    {
        return new BuffSelfIntent(self, statusEffect, stacks)
            .ToSingletonList<AbstractIntent>();
    }
}
