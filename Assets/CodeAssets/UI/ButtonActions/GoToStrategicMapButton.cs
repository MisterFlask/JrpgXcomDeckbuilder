using UnityEngine;
using System.Collections;

public class GoToStrategicMapButton : MonoBehaviour
{
    public void Fire()
    {
        ServiceLocator.GetUiStateManager().SwitchToUiState(new ShowingMapMessage());
    }
}
