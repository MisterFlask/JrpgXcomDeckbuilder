using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntentPrefab : MonoBehaviour
{
    public CustomGuiText Text;
    public Image Picture;
    public Intent UnderlyingIntent { get; set; }

    public void SetText(string text)
    {
        Text.SetText(text);
    }
}
