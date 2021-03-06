﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using HyperCard;
using System.Collections.Generic;
using System.Linq;
using System;
using Assets.CodeAssets.UI.Screens.BattleScreen;

public class CardUiBehaviors : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    AbstractCard logicalCard => this.GetComponent<PlayerCard>().LogicalCard;

    private Vector3 handleToOriginVector;
    public bool isDragging;

    ArrowController arrowController;

    void Update()
    {
        this.GetComponent<Card>().Refresh();
    }


    void Start()
    {
        arrowController = GameObject.FindObjectOfType<ArrowController>();
        if (arrowController == null)
        {
            return;
        }
        arrowController.enabled = false; //TODO:  Make this thing work right
        arrowController.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        var movement = this.GetComponent<CardMovementBehaviors>();
        var handManager = ServiceLocator.GetCardAnimationManager();
        handManager.MoveCardToAppropriateLocation(movement.GetComponent<Card>(), true);
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (isDragging)
        {
            return;
        }
        var movement = this.GetComponent<CardMovementBehaviors>();
        var handManager = ServiceLocator.GetCardAnimationManager();
        handManager.MoveCardToAppropriateLocation(movement.GetComponent<Card>(), false);
    }

    public void OnPointerDown(PointerEventData data)
    {
        //handleToOriginVector = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ServiceLocator.GameState().SetCardSelected(this.GetComponent<Card>());
        //arrowController.SetVisible(true);
        //arrowController.SetStart(this.transform.position);
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        isDragging = false;
        //arrowController.SetVisible(false);
        var movement = this.GetComponent<CardMovementBehaviors>();
        var handManager = ServiceLocator.GetCardAnimationManager();

        handManager.MoveCardToAppropriateLocation(movement.GetComponent<Card>(), false);

        SendMessageToFirstValidMouseButtonUpHandler(Input.mousePosition);
        ServiceLocator.GameState().SetCardSelected(null);
    }

    protected void SendMessageToFirstValidMouseButtonUpHandler(Vector2 position)
    {
        var elements = GetAllUIElements(position);
        if (elements == null)
        {
            return;
        }

        if (SelectCardInHandOverlay.INSTANCE.IsCardSelectBehaviorActive)
        {
            foreach (var element in elements)
            {
                var cardUiBehaviorNullable = element.GetComponent<CardUiBehaviors>();
                if (cardUiBehaviorNullable != null)
                {
                    var card = cardUiBehaviorNullable.GetComponent<Card>();
                    card.IsSelected = !card.IsSelected;
                    CardAnimationManager.INSTANCE.ReorientAllCards();
                    SelectCardInHandOverlay.INSTANCE.ExecuteClickingOnCard(cardUiBehaviorNullable);
                }
            }
            return;
        }

        foreach (var element in elements)
        {
            if (element.GetComponent<CardUiBehaviors>() != null)
            {
                // We're ignoring any case where we release the mouse button over a card
                return;
            }

            var battleUnitMousedOver = element.GetComponent<BattleUnitPrefab>();
            var battleUnitTargeted = battleUnitMousedOver?.UnderlyingEntity;

            BattleRules.ProcessUiReleasingCardOverBattleUnit(logicalCard, battleUnitTargeted);

            if (element.GetComponent<CardPlayArea>() != null && logicalCard.TargetType == TargetType.NO_TARGET_OR_SELF)
            {
                ActionManager.Instance.AttemptPlayCardFromHand(this.logicalCard, null);
            }

        }

        ServiceLocator.GameState().UnselectCard();
    }

    /// <summary>
    /// Handles the case where we're on a screen similar to "discard a card", where the user has to select a number of cards.
    /// These are sovereign screens, so only card selection actions are particularly relevant.
    /// </summary>
    private void HandleCardReleasedEventForCardSelect(List<GameObject> elements, AbstractCard logicalCard)
    {
        var card = logicalCard.FindCorrespondingHypercard();
        card.IsSelected = !card.IsSelected;

        ServiceLocator.GetCardAnimationManager().ReorientAllCards();
    }

    protected List<GameObject> GetAllUIElements(Vector2 position)
    {
        var uiRaycastResultCache = new List<RaycastResult>();

        var uiEventSystem = EventSystem.current;
        if (uiEventSystem != null)
        {

             var uiPointerEventData = new PointerEventData(uiEventSystem);
            uiPointerEventData.position = position;

            uiEventSystem.RaycastAll(uiPointerEventData, uiRaycastResultCache);
            if (uiRaycastResultCache.Count > 0)
            {
                return uiRaycastResultCache.Select(item => item.gameObject).ToList() ;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}

