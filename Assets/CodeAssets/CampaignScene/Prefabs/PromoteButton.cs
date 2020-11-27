using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PromoteButton : MonoBehaviour
{

    AbstractBattleUnit SelectedUnit => SelectableRosterCharacterPrefab.CurrentlySelected;
    public void ClickAction()
    {
        SelectedUnit.ChangeClass(RookieClass.GetClassesEligibleForPromotion().PickRandom());
    }
    public bool IsClickable()
    {
        if (SelectedUnit == null)
        {
            return false;
        }
        return (SelectedUnit.PromotionAvailable);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        this.GetComponent<Button>().interactable = IsClickable();
    }
}
