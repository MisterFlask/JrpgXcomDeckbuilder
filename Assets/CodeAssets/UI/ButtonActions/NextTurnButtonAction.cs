using UnityEngine;
using System.Collections;

public class NextTurnButtonAction : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextTurnButtonClicked()
    {
        ServiceLocator.GetActionManager().EndTurn();
    }

}
