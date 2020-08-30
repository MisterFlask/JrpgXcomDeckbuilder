
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

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

        BattleUnitAttributesHolder.BattleUnit = entity;
    }

    public void HideOrShowAsAppropriate()
    {
        if (UnderlyingEntity == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        this.HealthText.SetText($"HP: {UnderlyingEntity.CurrentHp}/{UnderlyingEntity.MaxHp}");
        this.FatigueText.SetText($"Fatigue: {UnderlyingEntity.CurrentFatigue}/{UnderlyingEntity.MaxFatigue}");
    }

}
