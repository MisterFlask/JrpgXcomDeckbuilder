using UnityEngine;
using System.Collections;

public abstract class AbstractMissionReward
{
    public string Description { get; set; } = "";

    public ProtoGameSprite ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon();
    public abstract void OnReward();
}
