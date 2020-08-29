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
        this.ProtoSprite = ImageUtils.ProtoSpriteFromGameIcon(color: Color.red);
        this.IsAiControlled = true;
    }

    public override Intent GetNextIntent()
    {
        return new AttackIntent(this, IntentTargeting.GetRandomPlayerUnit(), 5);
    }
}
