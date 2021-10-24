using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyIcon : MonoBehaviour
{
    public Image image;
    public TMPro.TextMeshProUGUI Text;
    private int lastKnownValue = -1;

    public static EnergyIcon Instance;

    public void Start()
    {
        image.color = image.color.WithAlpha(0f);
    }
    public void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        var currentEnergy = GameState.Instance.energy;
        if (lastKnownValue != currentEnergy)
        {
            Flash();
            Text.text = $"{currentEnergy}";
            lastKnownValue = currentEnergy;
        }
    }

    public void Flash()
    {
        image.AddTransientComponentAndPerformOperation<AppearDisappearImageAnimationPrefab>((anim) =>
        {
            anim.Begin(fadeoutTime: 1f, secondsToBeOpaque: 0f);
        });
    }
}
