using UnityEngine;
using System.Collections;

public abstract class AbstractMissionReward
{
    public abstract string Description();

    public ProtoGameSprite ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon();
    public abstract void OnReward();
}

public class MoneyMissionReward : AbstractMissionReward
{
    int quantity;
    public MoneyMissionReward(int quantity)
    {
        this.quantity = quantity;
    }

    public override string Description()
    {
        return $"Gain ${quantity}";
    }

    public override void OnReward()
    {
        GameState.Instance.money+=quantity;
    }
}
