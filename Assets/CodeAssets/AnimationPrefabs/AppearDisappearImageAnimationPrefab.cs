using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


//Assumption is that the image starts off invisible.

[RequireComponent(typeof(Image))]
public class AppearDisappearImageAnimationPrefab : MonoBehaviour
{
    public void Begin(Action thingToDoAfterFadingIn = null, Action thingToDoBeforeFadingOut = null, float fadeoutTime = .1f, float secondsToBeOpaque = 1f)
    {
        StartCoroutine(FadeImage(fadeoutTime, secondsToBeOpaque, thingToDoAfterFadingIn, thingToDoBeforeFadingOut));
    }

    Image img => this.GetComponent<Image>();
    IEnumerator FadeImage(float fadeOutTimeInSeconds, float secondsToBeOpaque, Action thingToDoAfterFadingIn = null, Action thingToDoBeforeFadingOut = null)
    {
        
        img.color = img.color.WithAlpha(1f);
        /*
        // loop over 1 second
        for (float i = 0; i <= fadeOutTimeInSeconds; i += Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
        */
        thingToDoAfterFadingIn?.Invoke();

        float secondsSpentOpaque = 0;
        while (secondsSpentOpaque < secondsToBeOpaque)
        {
            secondsSpentOpaque += Time.deltaTime;
            yield return null;
        }

        thingToDoBeforeFadingOut?.Invoke();

        for (float timeSpentFading = 0; timeSpentFading <= fadeOutTimeInSeconds; timeSpentFading += Time.deltaTime)
        {
            var fractionNotFaded = (fadeOutTimeInSeconds - timeSpentFading) / fadeOutTimeInSeconds;
            // set color with i as alpha
            img.color = img.color.WithAlpha(fractionNotFaded);
            yield return null;
        }
        img.color = img.color.WithAlpha(0);

        Destroy(this);
    }
}
