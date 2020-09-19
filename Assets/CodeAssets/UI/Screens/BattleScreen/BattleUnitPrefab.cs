﻿
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using System;

public class BattleUnitPrefab:MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CustomGuiText CharacterNameText;
    public Image SpriteImage;
    public BattleUnitAttributesHolder BattleUnitAttributesHolder;
    public CustomGuiText FatigueText;
    public CustomGuiText HealthText;
    public CustomGuiText DefenseText;

    public Transform IntentPrefabParent;

    public List<IntentPrefab> IntentPrefabs { get; set; } = new List<IntentPrefab>();

    public AbstractBattleUnit UnderlyingEntity { get; private set; }
    Color OriginalImageColor { get; set; }
    Color BrighterImageColor { get; set; }


    public void Initialize(AbstractBattleUnit entity)
    {
        var img = entity.ProtoSprite.ToGameSpriteImage();
        entity.CorrespondingPrefab = this;
        SpriteImage.sprite = img.Sprite;
        SpriteImage.color = img.Color;
        UnderlyingEntity = entity;

        BattleUnitAttributesHolder.BattleUnit = entity;

        OriginalImageColor = SpriteImage.color;
        BrighterImageColor = SpriteImage.color * 1.5f;
    }

    public void HideUnit()
    {
        this.UnderlyingEntity = null;
        HideOrShowAsAppropriate();
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
        if (UnderlyingEntity == null)
        {
            return;
        }
        this.CharacterNameText.SetText(UnderlyingEntity.CharacterName);
        this.HealthText.SetText($"HP: {UnderlyingEntity.CurrentHp}/{UnderlyingEntity.MaxHp}");
        this.FatigueText.SetText($"Fatigue: {UnderlyingEntity.CurrentFatigue}/{UnderlyingEntity.MaxFatigue}");
        this.DefenseText.SetText($"{UnderlyingEntity.CurrentDefense}");
        var intentsExistingInPrefabForm = this.IntentPrefabs.Select(item => item.UnderlyingIntent);
        var relevantIntents = IntentsRelevantToCharacter();
        foreach (var intent in relevantIntents)
        {
            if (intentsExistingInPrefabForm.Contains(intent))
            {
                // if it's already in our prefab list, great
                continue;
            }
            else
            {
                // if it's not, it needs to be added
                var newPrefab = intent.GeneratePrefabAndAssign(IntentPrefabParent.gameObject);
                IntentPrefabs.Add(newPrefab);
            }
        }

        var intentsToRemove = new List<IntentPrefab>();
        // if it's in our prefab list and isn't in our intents relevant to the character, we have to upadte it.
        foreach(var prefab in IntentPrefabs)
        {
            if (!relevantIntents.Contains(prefab.UnderlyingIntent))
            {
                intentsToRemove.Add(prefab);
            }
        }
        intentsToRemove.ForEach(item => IntentPrefabs.Remove(item)); // remove from list of prefabs
        intentsToRemove.ForEach(item => item.transform.parent = null); // remove from parent (thus removing from the UI)

        if (ShouldHighlight())
        {
            Highlight(this.SpriteImage);
        }
        else
        {
            RemoveHighlights(this.SpriteImage);
        }
    }

    private void RemoveHighlights(Image spriteImage)
    {
        spriteImage.color = OriginalImageColor;
    }

    private void Highlight(Image spriteImage)
    {
        spriteImage.color = BrighterImageColor;
    }

    private bool ShouldHighlight()
    {
        return BattleScreenPrefab.IntentMousedOver?.UnitsTargeted?.Contains(this.UnderlyingEntity) ?? false || BattleScreenPrefab.IntentMousedOver?.Source == this.UnderlyingEntity;
    }

    public List<Intent> IntentsRelevantToCharacter()
    {
        // intents where I am being TARGETED.
        var intentsAccumulator = new List<Intent>();

        foreach(var enemyCharacter in ServiceLocator.GetGameStateTracker().EnemyUnitsInBattle){
            var intentsForThisEnemy = enemyCharacter.CurrentIntents;
            intentsAccumulator.AddRange(intentsForThisEnemy.Where(item => item.UnitsTargeted.Contains(UnderlyingEntity)));
        }

        // My own intents
        var myOwnIntents = this.UnderlyingEntity.CurrentIntents ?? new List<Intent>();
        intentsAccumulator.AddRange(myOwnIntents);
        return intentsAccumulator;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered battle unit prefab; setting battle unit moused over");
        BattleScreenPrefab.BattleUnitMousedOver = this.UnderlyingEntity;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exited battle unit prefab; unsetting battle unit moused over");
        BattleScreenPrefab.BattleUnitMousedOver = null;
    }
}
