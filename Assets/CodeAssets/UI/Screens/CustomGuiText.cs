using UnityEngine;
using System.Collections;
using TMPro;

/// <summary>
/// This is just a wrapper over textmeshpro so I don't have to keep futzing around wtih it
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class CustomGuiText : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public void Start()
    {
        if (Text == null)
        {
            Text = GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetText(string textWeWant)
    {
        Text.text = textWeWant;
    }
}
