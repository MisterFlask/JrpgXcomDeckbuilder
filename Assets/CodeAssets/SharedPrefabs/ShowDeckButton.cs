using UnityEngine;
using System.Collections;

public class ShowDeckButton : MonoBehaviour
{
    public void ShowForCharacter()
    {
        ShowDeckScreen.ShowDeckForSelectedCharacter();
    }
    public void ShowDiscard()
    {
        ShowDeckScreen.ShowDiscardPile();
    }
    public void ShowDraw()
    {
        ShowDeckScreen.ShowDrawPile();
    }
    public void ShowExhaust()
    {
        ShowDeckScreen.ShowExhaustPile();
    }
}
