using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyIconGlow : MonoBehaviour
{
    public Image image;

    public static MoneyIconGlow Instance;

    public void Start()
    {
        Instance = this;
        image.color = image.color.WithAlpha(0f);
    }

    public void Flash()
    {
        image.AddComponentAndPerformOperation<AppearDisappearImageAnimationPrefab>((anim) =>
        {
            anim.Begin(fadeoutTime: 1f, secondsToBeOpaque: 0f);
        });
    }


}
