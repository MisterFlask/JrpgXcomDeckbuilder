using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractIntent
{
    public AbstractIntent(AbstractBattleUnit source,
        List<AbstractBattleUnit> unitsTargeted = null,
        ProtoGameSprite protoSprite = null)
    {
        this.ProtoSprite = protoSprite ?? ImageUtils.ProtoGameSpriteFromGameIcon(color: Color.blue);
        this.Source = source;
        this.UnitsTargeted = unitsTargeted ?? new List<AbstractBattleUnit>();
    }

    public string Id = Guid.NewGuid().ToString();


    public void ExecuteIntent()

    {
        if (Source.IsDead)
        {
            return;
        }
        Execute();
    }

    protected abstract void Execute();

    protected abstract IntentPrefab GeneratePrefab(GameObject parent);

    public IntentPrefab GeneratePrefabAndAssign(GameObject parent)
    {
        var prefab = GeneratePrefab(parent);
        prefab.UnderlyingIntent = this;
        prefab.Picture.SetProtoSprite(this.ProtoSprite);
        prefab.Init();
        return prefab;
    }

    // can be an empty list
    public List<AbstractBattleUnit> UnitsTargeted { get; set; } = new List<AbstractBattleUnit>();

    public AbstractBattleUnit Source { get; set; }

    public ProtoGameSprite ProtoSprite { get; set; }

    public static AbstractIntent GetIntentFromShuffle(List<AbstractIntent> options)
    {
         return options.Shuffle().First();
    }

    public static AbstractIntent GetIntentFromOrderedActions(List<AbstractIntent> optionsInOrder, int turnNumber)
    {
        var index = turnNumber % optionsInOrder.Count;
        return optionsInOrder[index];
    }

    public abstract string GetText();
}