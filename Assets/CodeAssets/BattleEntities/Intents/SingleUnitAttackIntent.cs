using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingleUnitAttackIntent : AbstractIntent
{
    public SingleUnitAttackIntent(AbstractBattleUnit Source,
                        AbstractBattleUnit Target,
                        int damage,
                        int numberOfTimesStruck = 1): base(Source, Target.ToSingletonList())
    {
        this.Source = Source;
        this.Target = Target;
        BaseDamage = damage;
        NumberOfTimesStruck = numberOfTimesStruck;
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(color: Color.red, path: "Sprites/crossed-swords");
    }

    public static SingleUnitAttackIntent AttackRandomEnemy(AbstractBattleUnit source, int damage, int numTimesStruck)
    {
        var target = IntentTargeting.GetRandomLivingPlayerUnit();
        if (target == null)
        {
            return null;
        }
        return new SingleUnitAttackIntent(source, target, damage, numTimesStruck);
    }

    private ActionManager action => ServiceLocator.GetActionManager();

    public AbstractBattleUnit Target { get; }
    public int BaseDamage { get; }
    public int NumberOfTimesStruck { get; }

    protected override IntentPrefab GeneratePrefab(GameObject parent)
    {
        var totalDamageExpected = BattleRules.GetAnticipatedDamageToUnit(Source, Target, BaseDamage);
        var parentPrefab = ServiceLocator.GameObjectTemplates().AttackPrefab;
        var returnedPrefab = parentPrefab.Spawn(parent.transform);
        returnedPrefab.Text.SetText($"{totalDamageExpected}x{NumberOfTimesStruck}");
        return returnedPrefab;
    }

    protected override void Execute()
    {
        for(int i = 0; i < NumberOfTimesStruck; i++)
        {
            action.AttackUnitForDamage(Target, Source, BaseDamage);
        }
    }

}
