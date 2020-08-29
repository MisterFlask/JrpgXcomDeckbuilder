using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BattleUnitAttributePrefab: MonoBehaviour
{
    public Image image;
    public CustomGuiText Text;

    public void Initialize(Sprite sprite, string text)
    {
        image.sprite = sprite;
        Text.SetText(text);
    }
}