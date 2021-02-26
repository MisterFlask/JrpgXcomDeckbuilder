using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SelectableRosterCharacterPrefab : MonoBehaviour, IPointerClickHandler
{
    public TMPro.TextMeshProUGUI Title;
    public Image CharacterImage;
    public Toggle Toggle;

    public AbstractBattleUnit Character { get; set; }

    public static AbstractBattleUnit CurrentlySelected => GameState.Instance.CharacterSelected;
    public static List<SelectableRosterCharacterPrefab> AllSelectableCharacterPrefabs => RosterPrefab.Instance.ChildPrefabs;
    public static IEnumerable<SelectableRosterCharacterPrefab> SelectedPrefabs => AllSelectableCharacterPrefabs.Where(item => item.Toggle.isOn);

    public void OnPointerClick(PointerEventData eventData)
    {
        GameState.Instance.CharacterSelected = this.Character;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Character == null)
        {
            return;
        }

        CharacterImage.SetProtoSprite(Character.ProtoSprite);
        if (Character.IsDead)
        {
            Toggle.isOn = false;
            Toggle.interactable = false;
            Title.text = Character.CharacterFullName + "<color=red>"  + " [Deceased]" + "</color>";
        }
        else
        {
            Title.text = Character.CharacterFullName + $" [{Character.SoldierClass.Name()}]";
        }
        if (CurrentlySelected == Character)
        {
            Title.color = Color.yellow;
        }
        else
        {
            Title.color = Color.black;
        }
    }
}
