using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MissionDescriptorPrefab : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Title;
    public TMPro.TextMeshProUGUI Description;
    public Button EmbarkButton;

    public static Mission SelectedMission => SelectableMissionPrefab.CurrentlySelected?.Mission;
    public static bool ShouldBeAbleToGoOnMission
    {
        get
        {
            if (SelectedMission == null)
            {
                return false;
            }

            return SelectedMission.MaxNumberOfFriendlyCharacters <= SelectableRosterCharacterPrefab.SelectedPrefabs.Count();
        }

    }

    public void Update()
    {
        if (SelectedMission == null)
        {
            Title.text = "Select a mission.";
            Description.text = "";
            return;
        }

        if (ShouldBeAbleToGoOnMission)
        {
            Title.text = SelectedMission.Name;
        }
        else
        {
            Title.text = SelectedMission.Name + $"[Unable to embark; too many characters.  Max is {SelectedMission?.MaxNumberOfFriendlyCharacters}]";
        }

        Description.text = SelectedMission.GenerateMissionDescriptiveText();
     }

    public void Start()
    {
        EmbarkButton.onClick.AddListener(() =>
        {
            // todo: Figure out scene transition.
            // Set up static stuff for combat.
            var selectedMission = GetMissionSelected();
            var charactersSent = GetCharactersSent();
            if (selectedMission == null || (charactersSent?.Count ?? 0) == 0)
            {
                if (selectedMission == null)
                {
                    Debug.Log("Can't go on mission; no mission selected.");
                }
                else
                {
                    Debug.Log("Can't go on mission; no characters selected.");
                }
                return;
            }
            GameScenes.SwitchToBattleScene(selectedMission, charactersSent);
        });
    }

    private List<AbstractBattleUnit> GetCharactersSent()
    {
        return RosterPrefab.Instance.GetCharactersSelected().ToList();
    }

    private Mission GetMissionSelected()
    {
        return SelectedMission;
    }
}
