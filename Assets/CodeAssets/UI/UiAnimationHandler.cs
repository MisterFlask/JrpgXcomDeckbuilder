using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiAnimationHandler : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public void PulseAndFlashElement(Image image, float duration = 1f, float scaleDiff = 1.4f)
    {
        StartCoroutine(PulseAndFlashElementAsync(image, duration, scaleDiff));
    }

    // Done to bring attention to changing UI elements
    public IEnumerator PulseAndFlashElementAsync(Image image, float duration = 1f, float scaleDiff = 1.4f)
    {
        var startScale = image.transform.localScale;
        var startingColor = image.color;
        var flashedColor = image.color * 3f;
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            image.transform.localScale = Vector3.Lerp(startScale * scaleDiff, startScale, counter / duration);
            image.color = Color.Lerp(startingColor, flashedColor, counter / duration);
            yield return null;
        }
    }
}
