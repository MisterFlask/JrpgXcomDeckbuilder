using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class Intent
{
    public Intent(AbstractBattleUnit source,
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
        prefab.Picture.sprite = ProtoSprite.ToGameSpriteImage().Sprite;
        prefab.Picture.color = ProtoSprite.ToGameSpriteImage().Color;
        prefab.Init();
        return prefab;
    }

    // can be an empty list
    public List<AbstractBattleUnit> UnitsTargeted { get; set; } = new List<AbstractBattleUnit>();

    public AbstractBattleUnit Source { get; set; }

    public ProtoGameSprite ProtoSprite { get; set; }
}