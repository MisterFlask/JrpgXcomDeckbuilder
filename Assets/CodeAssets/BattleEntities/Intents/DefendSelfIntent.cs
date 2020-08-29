using UnityEngine;
using System.Collections;

public class DefendSelfIntent : Intent
{
    public override GameObject GeneratePrefab(GameObject parent)
    {
        var parentPrefab = ServiceLocator.GameObjectTemplates().DefendPrefab;
        return parentPrefab.Spawn(parent.transform).gameObject;
    }

    public override void Execute()
    {

    }
}