using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyIcon : MonoBehaviour
{
    public Image image;
    public TMPro.TextMeshProUGUI Text;

    private int lastKnownValue = -1;
    public static MoneyIcon Instance;

    public void Start()
    {
        Instance = this;
        image.color = image.color.WithAlpha(0f);
    }

    public void Update()
    {
        var currentMoney = GameState.Instance.Credits;
        if (lastKnownValue != currentMoney)
        {
            Flash();
            Text.text = $"{currentMoney}";
            lastKnownValue = currentMoney;
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
