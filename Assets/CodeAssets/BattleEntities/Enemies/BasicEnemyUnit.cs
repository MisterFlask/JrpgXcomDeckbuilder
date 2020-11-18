using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicEnemyUnit : AbstractBattleUnit
{
    public BasicEnemyUnit()
    {
        this.MaxHp = 20;
        this.CurrentHp = 20;
        this.CharacterName = "Basic Enemy";
        this.IsAlly = false;
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(color: Color.red);
        this.IsAiControlled = true;
        this.StatusEffects.Add(new DyingStatusEffect());
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        return new List<AbstractIntent> { new SingleUnitAttackIntent(this, IntentTargeting.GetRandomLivingPlayerUnit(), 5) };
    }
}
