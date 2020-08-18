using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using ModelShark;
using HyperCard;

[RequireComponent(typeof(CardAnimationManager))]
public class CardInstantiator : MonoBehaviour
{
    public TooltipStyle tooltipStyle;

    Card cardTemplate;
    Canvas canvas;
    Transform cardSpawnPoint;
    GameObject hand;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Called start method on instantiator");
        cardSpawnPoint = GameObject.Find("CARD_SPAWN_POINT").transform;
        var cardTemplateObj = GameObject.Find("CARD_TEMPLATE");
        cardTemplate = cardTemplateObj.GetComponent<Card>();
        canvas = GameObject.FindObjectOfType<Canvas>();
        Require.NotNull(canvas);
        Require.NotNull(cardTemplate);
        hand = GameObject.Find("Hand");
        ScriptExecutionTracker.HasCardInstantiatorLoaded = true;
        ScriptExecutionTracker.ScriptsFinishedExecuting.Add(nameof(CardInstantiator));
    }
    private int defaultStencilValue = 2;

    private int currentStencilValue = 4;

    public Card CreateCard()
    {
        
        var startTime = DateTime.Now;
        Card newCard = null;
        Profiler.Profile("SpawnCard", () =>
        {
            newCard = ServiceLocator.GetSpawnPool().Spawn("CARD_TEMPLATE", pos: cardSpawnPoint.position, rot: cardSpawnPoint.rotation, parent: hand.transform).GetComponent<Card>();
        });

        if (newCard.GetComponent<PlayerCard>() == null) // this basically just corresponds to whether in general it has all its components
        {
            newCard.gameObject.AddComponent<PlayerCard>();
            newCard.gameObject.AddComponent<CardMovementBehaviors>();
            newCard.gameObject.AddComponent<CardUiBehaviors>();
        }

        newCard.gameObject.GetComponent<CardUiBehaviors>().enabled = true;
        newCard.IsMovedToSelectionArea = false;
        newCard.transform.localScale = CardAnimationManager.NaturalCardSize;
        Profiler.Profile("ResetCardBehaviors", () =>
        {
            // reset behaviors
            ResetCardBehaviors(newCard);
        });
        /*
        var frontRenderer = newCard.Properties.FaceSide.Renderer;
        if (frontRenderer.GetComponent<TooltipTrigger>() == null)
        {
            frontRenderer.gameObject.AddComponent<TooltipTrigger>();
        }
        var tooltipTrigger = frontRenderer.GetComponent<TooltipTrigger>();
        tooltipTrigger.SetText("BodyText", TooltipGenerator.GetTooltipForCard(abstractCard));
        tooltipTrigger.tooltipStyle = this.tooltipStyle;
        */
        //TooltipManager.Instance.positionBounds = PositionBounds.Renderer;
        var endTime = DateTime.Now;
        var duration = endTime - startTime;
        Debug.Log("Instantiated card in " + duration.TotalMilliseconds);
        return newCard;
    }

    private void ResetCardBehaviors(Card newCard)
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
