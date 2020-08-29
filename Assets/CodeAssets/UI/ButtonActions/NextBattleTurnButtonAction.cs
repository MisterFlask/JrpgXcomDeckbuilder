using UnityEngine;
using System.Collections;

public class NextBattleTurnButtonAction : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextBattleTurnButtonClicked()
    {
        ServiceLocator.GetActionManager().EndBattleTurn();
    }

}
