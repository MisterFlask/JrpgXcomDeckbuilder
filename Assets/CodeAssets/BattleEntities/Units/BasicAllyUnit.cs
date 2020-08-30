using UnityEngine;
using System.Collections;

public class BasicAllyUnit : AbstractBattleUnit
{
    public BasicAllyUnit()
    {
        this.MaxHp = 20;
        this.CurrentHp = 20;
        this.Name = "Basic Ally";
        this.IsAlly = true;
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(color:Color.blue);
        this.IsAiControlled = false;
    }

    public override Intent GetNextIntent()
    {
        throw new System.NotImplementedException();
    }
}
