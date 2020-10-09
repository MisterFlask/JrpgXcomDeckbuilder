using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


//Assumption is that the image starts off invisible.

[RequireComponent(typeof(Image))]
public class AppearDisappearImageAnimationPrefab : MonoBehaviour
{
    public void Begin(Action thingToDoAfterFadingIn = null, Action thingToDoBeforeFadingOut = null)
    {
        StartCoroutine(FadeImage(.1f, 1f, thingToDoAfterFadingIn, thingToDoBeforeFadingOut));
    }

    Image img => this.GetComponent<Image>();
    IEnumerator FadeImage(float transitionTimeInSeconds, float secondsToBeOpaque, Action thingToDoAfterFadingIn = null, Action thingToDoBeforeFadingOut = null)
    {
        // loop over 1 second
        for (float i = 0; i <= transitionTimeInSeconds; i += Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
        thingToDoAfterFadingIn?.Invoke();

        float secondsSpentOpaque = 0;
        while (secondsSpentOpaque < secondsToBeOpaque)
        {
            secondsSpentOpaque += Time.deltaTime;
            yield return null;
        }

        thingToDoBeforeFadingOut?.Invoke();

        // loop over 1 second backwards
        for (float i = transitionTimeInSeconds; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }

        Destroy(this);
    }
}
