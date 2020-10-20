using UnityEngine;
using System.Collections;
using System;
using HyperCard;

[RequireComponent(typeof(Card))]
public class CardMovementBehaviors: MonoBehaviour
{

    float startingCardScale = 100f;
    public void MoveAndShrinkToLocation(Vector3 moveTo)
    {
        KillMovement();
        StartCoroutine(MoveToPosition(this.transform.localPosition, moveTo, this.transform.localEulerAngles, .3f, this.transform.localScale, true, null));
    
    }

    public void MoveToLocation(Vector3 moveTo, Vector3? rotateTo = null, Vector3? newLocalScale = null)
    {
        KillMovement();
        var toRotation = rotateTo;
        if (toRotation == null) {
            toRotation = new Vector3(0,0,0);
        }
        StartCoroutine(MoveToPosition(this.transform.localPosition, moveTo, toRotation.Value, .3f, this.transform.localScale, false, newLocalScale));
    }

    public void DissolveAndDestroyCard()
    {

        if (Disappearing)
        {
            return;
        }
        Disappearing = true;
        StartCoroutine(DisappearSlowly(1f));
    }

    bool Disappearing = false;

    IEnumerator DisappearSlowly(float duration)
    {
        yield return null;
        var card = GetComponent<Card>();

        var counter = 0f;
        while (counter < duration)
        {
            counter += Time.deltaTime / duration;
            // TODO:  Some kind of animation
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Finished animation, destroying object");
        Disappearing = false;
        this.gameObject.Despawn();
    }

    public void KillMovement()
    {
        killSwitch = true;
        isMoving = false;
    }

    bool killSwitch = false;
    bool isMoving = false;

    public IEnumerator MoveToPosition(Vector3 fromPosition, Vector3 toPosition, Vector3 toRotation, float duration, Vector3 startingScale, bool shrink, Vector3? newLocalScale)
    {
        killSwitch = false;
        //Make sure there is only one instance of this function running
        if (isMoving)
        {
            yield break; ///exit if this is still running
        }
        isMoving = true;

        float counter = 0;

        //Get the current position of the object to be moved
        Vector2 startPos = fromPosition;
        Vector2 startRot = this.transform.localRotation.eulerAngles;

        while (counter < duration)
        {

            if (killSwitch)
            {
                Debug.Log("Kill switch thrown");
                killSwitch = false;
                isMoving = false;
                break;
            }
            counter += Time.deltaTime;
            this.transform.localPosition = Vector3.Lerp(startPos, toPosition, counter / duration);
            this.transform.eulerAngles = Vector3.Lerp(startRot, toRotation, counter / duration);
            if (shrink)
            {
                this.transform.localScale = Vector3.Lerp(startingScale, Vector3.zero, counter / duration);
            }
            else if (newLocalScale != null)
            {
                this.transform.localScale = Vector3.Lerp(startingScale, newLocalScale.Value, counter / duration);
            }
            yield return null;
        }

        isMoving = false;

        if (shrink)
        {
            Debug.Log("Finished shrink animation, destroying object");

            this.gameObject.Despawn();
        }
        
    }
}