using UnityEngine;
using System.Collections;

public class BasicEnemyUnit : AbstractBattleUnit
{
    public BasicEnemyUnit()
    {
        this.MaxHp = 20;
        this.CurrentHp = 20;
        this.Name = "Basic Enemy";
        this.IsAlly = false;
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(color: Color.red);
        this.IsAiControlled = true;
        this.Attributes.Add(new DyingStatusEffect());
    }

    public override Intent GetNextIntent()
    {
        return new AttackIntent(this, IntentTargeting.GetRandomPlayerUnit(), 5);
    }
}
