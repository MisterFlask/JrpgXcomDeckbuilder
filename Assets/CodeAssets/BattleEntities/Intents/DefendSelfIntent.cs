﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefendSelfIntent : AbstractIntent
{
    private int baseDefenseGranted;
    public DefendSelfIntent(AbstractBattleUnit source, int baseDefenseGranted) : base(source, source.ToSingletonList())
    {
        this.baseDefenseGranted = baseDefenseGranted;
    }

    protected override IntentPrefab GeneratePrefab(GameObject parent)
    {
        var parentPrefab = ServiceLocator.GameObjectTemplates().DefendPrefab;
        return parentPrefab.Spawn(parent.transform);
    }

    protected override void Execute()
    {

    }

    public override string GetText()
    {
        return $"";
    }
}