using UnityEngine;
using System.Collections;

public abstract class SimpleIntent : AbstractIntent
{

    public SimpleIntent(AbstractBattleUnit source, ProtoGameSprite protoSprite): base(source)
    {
        this.ProtoSprite = protoSprite;
    }

    public override string GetText()
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
