
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using ModelShark;
using Assets.CodeAssets.UI.Tooltips;
using MoreMountains.Feedbacks;
using Assets.CodeAssets.UI;
using UnityEngine.UI;

public class BattleUnitPrefab:MonoBehaviour
{
    public CustomGuiText CharacterNameText;
    public Image SpriteImage;
    public BattleUnitAttributesHolder BattleUnitAttributesHolder;
    public CustomGuiText FatigueText;
    public CustomGuiText HealthText;
    public CustomGuiText DefenseText;
    public Image DefenseImage;

    public Transform IntentPrefabParent;
    public Button AdvanceOrRetreatButton;
    public CustomGuiText AdvanceOrRetreatButtonText;

    public Image SpeechBubble;
    public CustomGuiText SpeechBubbleText;
    public List<IntentPrefab> IntentPrefabs { get; set; } = new List<IntentPrefab>();

    public AbstractBattleUnit UnderlyingEntity { get; private set; }
    public Image highlights;


    public TooltipTriggerController TooltipController;
    public TooltipTrigger TooltipTrigger;

    public Image EmblemImage;

    public BattleUnitPrefabHardpoints Hardpoints;
    public MMFeedbacks FlickerFeedbacks;


    public SpriteRenderer SpriteImage_Worldspace;
    
    public MMFeedbacks DeathRotationFeedbacks;
    public MMFeedbacks HighlightUnitFeedbacks;
    public MMFeedbacks FloatingTextFeedbacks;

    Color OriginalImageColor { get; set; }
    Color BrighterImageColor { get; set; }

    public string NameOfCharacter;//Set for debugging
    public void Initialize(AbstractBattleUnit entity)
    {
        NameOfCharacter = entity.CharacterFullName ?? entity.GetDisplayName(DisplayNameType.SHORT_NAME);
        SpriteImage.SetProtoSprite(entity.ProtoSprite);
        SpriteImage_Worldspace.SetProtoSprite(entity.ProtoSprite);
        entity.CorrespondingPrefab = this;
        UnderlyingEntity = entity;

        BattleUnitAttributesHolder.BattleUnit = entity;

        OriginalImageColor = SpriteImage.color;
        BrighterImageColor = SpriteImage.color * 1.5f;

        highlights.gameObject.SetActive(false);
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

        if (UnderlyingEntity.SoldierClass != null && !UnderlyingEntity.IsEnemy)
        {
            EmblemImage.gameObject.SetActive(true);
            EmblemImage.sprite = UnderlyingEntity.SoldierClass.EmblemIcon.ToSprite();
        }
        else
        {
            EmblemImage.gameObject.SetActive(false);
        }

        // no advance or retreat button
        this.AdvanceOrRetreatButton.gameObject.SetActive(false);
        this.AdvanceOrRetreatButtonText.gameObject.SetActive(false);
        FatigueText.gameObject.SetActive(false);
        /*
        if (UnderlyingEntity.IsAiControlled)
        {
            this.AdvanceOrRetreatButton.gameObject.SetActive(false);
            this.AdvanceOrRetreatButtonText.gameObject.SetActive(false);
        }
        else if (BattleRules.CanAdvance(this.UnderlyingEntity))
        {
            this.AdvanceOrRetreatButton.interactable = true;
        }
        else if (BattleRules.CanFallBack(this.UnderlyingEntity))
        {
            this.AdvanceOrRetreatButton.interactable = true;
        }
        else
        {
            this.AdvanceOrRetreatButton.interactable = false;
        }
        */

        var buttonText = BattleRules.GetAdvanceOrFallBackButtonText(this.UnderlyingEntity);
        this.AdvanceOrRetreatButtonText.SetText(buttonText);

        if (UnderlyingEntity.CurrentBlock > 0)
        {
            DefenseText.gameObject.SetActive(true);
            DefenseImage.gameObject.SetActive(true);
        }
        else
        {
            DefenseText.gameObject.SetActive(false);
            DefenseImage.gameObject.SetActive(false);
        }

        if (UnderlyingEntity.IsEnemy)
        {
            CharacterNameText.SetText("");
        }
        else
        {
            this.CharacterNameText.SetText(UnderlyingEntity.GetDisplayName(DisplayNameType.SHORT_NAME));
        }

        this.HealthText.SetText($"HP: {UnderlyingEntity.CurrentHp}/{UnderlyingEntity.MaxHp}");
        // this.FatigueText.SetText($"Fatigue: {UnderlyingEntity.CurrentFatigue}/{UnderlyingEntity.MaxFatigue}");
        this.DefenseText.SetText($"{UnderlyingEntity.CurrentBlock}");
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
        intentsToRemove
                .Where(item => item != null)
                .ToList()
                .ForEach(item => item.transform.SetParent(null)); // remove from parent (thus removing from the UI)

        if (ShouldHighlight())
        {
            if (!HighlightUnitFeedbacks.IsPlaying)
            {
                HighlightUnitFeedbacks.PlayFeedbacks();
            }
        }
        else
        {
            highlights.gameObject.SetActive(false);
        }

        if (UnderlyingEntity.IsAiControlled)
        {
            // remove all the player interaction widgets
            this.AdvanceOrRetreatButton.gameObject.SetActive(false);
            this.FatigueText.gameObject.SetActive(false);
        }

        UnderlyingEntity.PerformStateBasedActions();
    }

    private void RemoveHighlights(Image spriteImage)
    {
        // spriteImage.color = OriginalImageColor;
    }

    private void Highlight(Image spriteImage)
    {

        // spriteImage.color = BrighterImageColor;
    }

    private bool ShouldHighlight()
    {
        var intentIsRelevant = BattleScreenPrefab.IntentMousedOver?.UnitsTargeted?.Contains(this.UnderlyingEntity) ?? false
            || BattleScreenPrefab.IntentMousedOver?.Source == this.UnderlyingEntity;
        var unitIsRelevant = BattleScreenPrefab.CardMousedOver?.Owner == this.UnderlyingEntity;

        return intentIsRelevant || unitIsRelevant;
    }

    public List<AbstractIntent> IntentsRelevantToCharacter()
    {
        // intents where I am being TARGETED.
        var intentsAccumulator = new List<AbstractIntent>();

        foreach(var enemyCharacter in ServiceLocator.GameState().EnemyUnitsInBattle){
            var intentsForThisEnemy = enemyCharacter.CurrentIntents;
            if (intentsForThisEnemy == null) continue;
            intentsAccumulator.AddRange(intentsForThisEnemy.Where(item => item.UnitsTargeted!= null && item.UnitsTargeted.Contains(UnderlyingEntity)));
        }

        // My own intents
        var myOwnIntents = this.UnderlyingEntity.CurrentIntents ?? new List<AbstractIntent>();
        intentsAccumulator.AddRange(myOwnIntents);
        return intentsAccumulator;
    }

    public void OnPrefabEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Debug.Log("Entered battle unit prefab; setting battle unit moused over");
        BattleScreenPrefab.BattleUnitMousedOver = this.UnderlyingEntity;
        ExplainerPanel.ShowBattleUnitHelp(this);
        TooltipController.ShowTooltipForBattleUnitClass(this.UnderlyingEntity);
        TooltipController.GetComponent<TooltipTrigger>().enabled = true;
    }

    public void OnPrefabExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Debug.Log("Exited battle unit prefab; unsetting battle unit moused over");
        if (BattleScreenPrefab.BattleUnitMousedOver == this.UnderlyingEntity)
        {
            BattleScreenPrefab.BattleUnitMousedOver = null;
        }
        ExplainerPanel.Hide();
    }

    public void OnPrefabClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        ServiceLocator.GetActionManager().Shout(this.UnderlyingEntity, "Clicked on unit.");
    }
}
