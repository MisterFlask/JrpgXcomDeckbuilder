using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyIconGlow : MonoBehaviour
{
    public Image image;

    public static EnergyIconGlow Instance;

    public void Start()
    {
        Instance = this;
        image.color = image.color.WithAlpha(0f);
    }

    public void Flash()
    {
        image.AddComponentAndPerformOperation<AppearDisappearImageAnimationPrefab>((anim) =>
        {
            anim.Begin(fadeoutTime: 2f, secondsToBeOpaque: 0f);
        });
    }


}
