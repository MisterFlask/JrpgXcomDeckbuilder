using UnityEngine;
using System.Collections;

public class ShowDeckButton : MonoBehaviour
{
    AbstractBattleUnit SelectedUnit => SelectableRosterCharacterPrefab.CurrentlySelected;
    public void ShowForCharacter()
    {
        if (SelectedUnit == null)
        {
            return;
        }
        ShowDeckScreen.ShowDeckForSelectedCharacter();
    }

    public void ShowDiscard()
    {
        if (SelectedUnit == null)
        {
            return;
        }
        ShowDeckScreen.ShowDiscardPile();
    }

    public void ShowDraw()
    {
        if (SelectedUnit == null)
        {
            return;
        }
        ShowDeckScreen.ShowDrawPile();
    }

    public void ShowExhaust()
    {
        if (SelectedUnit == null)
        {
            return;
        }
        ShowDeckScreen.ShowExhaustPile();
    }
}
