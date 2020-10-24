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

    public IEnumerable<AbstractBattleUnit> GetCharactersSelected()
    {
        return SelectableRosterCharacterPrefab.SelectedPrefabs.Select(item => item.Character);
    }

    public RosterPrefab()
    {
    }

    public void Initialize()
    {
        CharacterSelectorPrefabParent.gameObject.PurgeChildren();
        foreach (var character in CampaignMapState.Roster)
        {
            AddCharacter(character);
        }
    }


    public void AddCharacter(AbstractBattleUnit unit)
    {
        var unitPrefab = PrefabTemplate.Spawn(CharacterSelectorPrefabParent);
        unitPrefab.Character = unit;
        ChildPrefabs.Add(unitPrefab);
    }
}
