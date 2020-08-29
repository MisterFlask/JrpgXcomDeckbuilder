using UnityEngine;
using System.Collections;

public class AttackIntent : Intent
{
    public AttackIntent(AbstractBattleUnit Source,
                        AbstractBattleUnit Target,
                        int damage,
                        int numberOfTimesStruck = 1)
    {
        this.Source = Source;
        this.Target = Target;
        Damage = damage;
        NumberOfTimesStruck = numberOfTimesStruck;
    }

    private ActionManager action => ServiceLocator.GetActionManager();

    public AbstractBattleUnit Source { get; }
    public AbstractBattleUnit Target { get; }
    public int Damage { get; }
    public int NumberOfTimesStruck { get; }

    public override GameObject GeneratePrefab(GameObject parent)
    {
        var parentPrefab = ServiceLocator.GameObjectTemplates().AttackPrefab;
        return parentPrefab.Spawn(parent.transform).gameObject;
    }

    public override void Execute()
    {
        for(int i = 0; i < NumberOfTimesStruck; i++)
        action.DamageUnit(Target, Damage);
    }
}
