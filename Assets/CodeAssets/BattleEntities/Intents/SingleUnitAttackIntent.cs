using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingleUnitAttackIntent : Intent
{
    public SingleUnitAttackIntent(AbstractBattleUnit Source,
                        AbstractBattleUnit Target,
                        int damage,
                        int numberOfTimesStruck = 1): base(Source, Target.ToSingletonList())
    {
        this.Source = Source;
        this.Target = Target;
        Damage = damage;
        NumberOfTimesStruck = numberOfTimesStruck;
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(color: Color.red, path: "Sprites/crossed-swords");
    }

    private ActionManager action => ServiceLocator.GetActionManager();

    public AbstractBattleUnit Target { get; }
    public int Damage { get; }
    public int NumberOfTimesStruck { get; }

    protected override IntentPrefab GeneratePrefab(GameObject parent)
    {
        var parentPrefab = ServiceLocator.GameObjectTemplates().AttackPrefab;
        var returnedPrefab = parentPrefab.Spawn(parent.transform);
        returnedPrefab.Text.SetText($"{Damage}x{NumberOfTimesStruck}");
        return returnedPrefab;
    }

    protected override void Execute()
    {
        for(int i = 0; i < NumberOfTimesStruck; i++)
        {
            action.AttackUnitForDamage(Target, Damage);
        }
    }

}
