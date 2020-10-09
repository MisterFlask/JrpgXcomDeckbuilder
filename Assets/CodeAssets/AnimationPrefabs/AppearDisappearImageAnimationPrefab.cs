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
        StartCoroutine(FadeImage(.4f, 3f, thingToDoAfterFadingIn, thingToDoBeforeFadingOut));
    }

    Image img => this.GetComponent<Image>();
    IEnumerator FadeImage(float timeToFadeInSeconds, float secondsToWaitBeforeFading, Action thingToDoAfterFadingIn = null, Action thingToDoBeforeFadingOut = null)
    {
        // loop over 1 second
        for (float i = 0; i <= timeToFadeInSeconds; i += Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
        if (thingToDoAfterFadingIn != null)
        {
            thingToDoAfterFadingIn();
        }
        float timeSpentOpaque = 0;
        while (timeSpentOpaque < secondsToWaitBeforeFading)
        {
            timeSpentOpaque += Time.deltaTime;
        }
        if (thingToDoBeforeFadingOut != null)
        {
            thingToDoBeforeFadingOut();
        }

        // loop over 1 second backwards
        for (float i = timeToFadeInSeconds; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }

        Destroy(this);
    }
}
