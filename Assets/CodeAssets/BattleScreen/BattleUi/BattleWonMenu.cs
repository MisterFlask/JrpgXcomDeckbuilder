using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleWonMenu : MonoBehaviour
{
    public Button ContinueButton;
    public BattleWonMenu()
    {
        ServiceLocator.MenuHolder.BattleWonMenu = this;
    }

    // Use this for initialization
    void Start()
    {
        this.ContinueButton.onClick.AddListener(() => {
            GameScenes.SwitchToBattleResultSceneAndProcessCombatResults(CombatResult.VICTORY);
        });

        Hide();
    }

    public void Display()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
