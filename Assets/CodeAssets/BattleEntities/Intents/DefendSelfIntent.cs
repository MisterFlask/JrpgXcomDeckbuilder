using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefendSelfIntent : Intent
{
    public DefendSelfIntent(AbstractBattleUnit source) : base(source, source.ToSingletonList())
    {
    }

    protected override IntentPrefab GeneratePrefab(GameObject parent)
    {
        var parentPrefab = ServiceLocator.GameObjectTemplates().DefendPrefab;
        return parentPrefab.Spawn(parent.transform);
    }

    public override void Execute()
    {

    }
}