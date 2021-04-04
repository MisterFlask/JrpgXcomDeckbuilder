using UnityEngine;
using System.Collections;
using HighlightPlus2D;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using HyperCard;

public class ClickableTile : MonoBehaviour
{
    public int X { get; set; }
    public int Y { get; set; }

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.AddComponent<HighlightEffect2D>();
        gameObject.AddComponent<HighlightTrigger2D>();
        var effect = gameObject.GetComponent<HighlightEffect2D>();
        TurnOffOnTopEffects(gameObject);

        var gameState = ServiceLocator.GameState();
    }
    private static void TurnOffOnTopEffects(GameObject g)
    {
        var gHighlights = g.GetComponent<HighlightEffect2D>();
        gHighlights.glowOnTop = false;
        gHighlights.outlineOnTop = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {

    }
    void OnMouseDown()
    {
        var cardsUnderMousePointer = CameraUtils.GetObjectsUnderMouse().ToComponents<Card>();

        if (!cardsUnderMousePointer.IsEmpty())
        {
            return;
        }
        Debug.Log($"Mouse down on {X}, {Y}");
        // this.GetComponent<MissionPopupCreator>().CreatePopup(this);
        //ServiceLocator.GetGameStateTracker().AddMission(new GatherFoodMission());
        //ServiceLocator.GetGameStateTracker().SetMissionSelected(new GatherFoodMission());
    }

}
