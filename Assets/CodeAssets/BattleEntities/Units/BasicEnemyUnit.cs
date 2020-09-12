using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicEnemyUnit : AbstractBattleUnit
{
    public BasicEnemyUnit()
    {
        this.MaxHp = 20;
        this.CurrentHp = 20;
        this.UnitClassName = "Basic Enemy";
        this.IsAlly = false;
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(color: Color.red);
        this.IsAiControlled = true;
        this.Attributes.Add(new DyingStatusEffect());
    }

    public override List<Intent> GetNextIntents()
    {
        return new List<Intent> { new SingleUnitAttackIntent(this, IntentTargeting.GetRandomPlayerUnit(), 5) };
    }
}
