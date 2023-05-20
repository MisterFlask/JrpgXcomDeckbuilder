using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RosterPrefab : MonoBehaviour
{
    public Transform CharacterSelectorPrefabParent;
    public SelectableRosterCharacterPrefab PrefabTemplate;
    public List<SelectableRosterCharacterPrefab> ChildPrefabs { get; set; } = new List<SelectableRosterCharacterPrefab>();
    public static RosterPrefab Instance => GameObject.FindObjectOfType<RosterPrefab>();
    public RosterPrefabType RosterType = RosterPrefabType.ENTIRE_ROSTER;
    public IEnumerable<AbstractBattleUnit> GetCharactersSelected()
    {
        return SelectableRosterCharacterPrefab.SelectedPrefabs.Select(item => item.Character);
    }

    public RosterPrefab()
    {
    }
    
    public void Initialize()
    {
        Debug.Log("Initializing roster prefab.");
        CharacterSelectorPrefabParent.gameObject.PurgeChildren();
        if (RosterType == RosterPrefabType.ENTIRE_ROSTER)
        {
            foreach (var character in GameState.Instance.PersistentCharacterRoster)
            {
                AddCharacter(character);
            }
        }
        else if (RosterType == RosterPrefabType.ONLY_CHARS_ON_THIS_RUN)
        {
            foreach (var character in GameState.Instance.AllyUnitsSentOnRun)
            {
                AddCharacter(character);
            }
        }
    }


    public void AddCharacter(AbstractBattleUnit unit)
    {
        var unitPrefab = PrefabTemplate.Spawn(CharacterSelectorPrefabParent);
        unitPrefab.Character = unit;
        ChildPrefabs.Add(unitPrefab);
    }

}

public enum RosterPrefabType
{
    ENTIRE_ROSTER,
    ONLY_CHARS_ON_THIS_RUN
}