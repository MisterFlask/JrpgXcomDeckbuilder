using UnityEngine;
using System.Collections;
using TMPro;

/// <summary>
/// This is just a wrapper over textmeshpro so I don't have to keep futzing around wtih it
/// </summary>
[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class CustomGuiText : MonoBehaviour
{
    TextMeshProUGUI text;
    public void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
    }
    public void SetText(string textWeWant)
    {
        text.text = textWeWant;
    }
}
