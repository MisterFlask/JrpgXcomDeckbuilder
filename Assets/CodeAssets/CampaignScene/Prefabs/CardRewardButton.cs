using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CardRewardButton : MonoBehaviour
{
    AbstractBattleUnit SelectedUnit => SelectableRosterCharacterPrefab.CurrentlySelected;


    bool IsEligible()
    {
        if (SelectedUnit == null)
        {
            return false;
        }
        return SelectedUnit.NumberCardRewardsEligibleFor > 0;
    }

    public void ClickAction()
    {
        ActionManager.Instance.PromptCardReward(SelectedUnit);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Button>().interactable = IsEligible();
    }
}
