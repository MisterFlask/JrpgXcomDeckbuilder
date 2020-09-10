using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleWonMenu : MonoBehaviour
{
    Button ContinueButton;
    public BattleWonMenu()
    {
        ServiceLocator.MenuHolder.BattleWonMenu = this;
    }

    // Use this for initialization
    void Start()
    {
        this.ContinueButton.onClick.AddListener(() => { 
            
        });
    }

    public void Display()
    {
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
