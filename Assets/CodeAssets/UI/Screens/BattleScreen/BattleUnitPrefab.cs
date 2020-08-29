
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleUnitPrefab:MonoBehaviour
{
    public Image SpriteImage;
    public BattleUnitAttributesHolder BattleUnitAttributesHolder;
    public CustomGuiText FatigueText;
    public CustomGuiText HealthText;

    public AbstractBattleUnit UnderlyingEntity { get; private set; }

    public void Initialize(AbstractBattleUnit entity)
    {
        var img = entity.ProtoSprite.ToGameSpriteImage();
        SpriteImage.sprite = img.Sprite;
        SpriteImage.color = img.Color;
        UnderlyingEntity = entity;
    }
}

public class BattleUnitAttributesHolder: MonoBehaviour
{
    public void AddBattleAttribute(AbstractBattleUnitAttribute attr)
    {
        
    }
    public void RemoveBattleAttribute(AbstractBattleUnitAttribute attr)
    {

    }
}