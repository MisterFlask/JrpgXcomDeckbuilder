using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NormalBoardUiState : CardSelectingBoardUiState
{
    public NormalBoardUiState()
    {
        this.AllowsNormalActions = true;
    }
    protected override void HandleCardReleasedEventForCardSelect(List<GameObject> elements, AbstractCard logicalCard)
    {
        throw new System.NotImplementedException();
    }
}
