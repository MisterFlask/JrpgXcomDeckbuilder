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
    public Image terrainImage;

    public static AbstractMission SelectedMission => SelectableMissionPrefab.CurrentlySelected?.Mission;
    public static bool ShouldBeAbleToGoOnMission(out string reasonWhyNot)
    {
        if (SelectedMission == null)
        {
            reasonWhyNot = "No mission selected!";
            return false;
        }

        var selectedNumberOfCharacters = SelectableRosterCharacterPrefab.SelectedPrefabs.Count();

        if (selectedNumberOfCharacters == 0)
        {
            reasonWhyNot = "No characters selected";
            return false;
        }
        if (SelectedMission.MaxNumberOfFriendlyCharacters < selectedNumberOfCharacters)
        {
            reasonWhyNot = $"Too many charactes for mission max ({SelectedMission.MaxNumberOfFriendlyCharacters})";

            return false;
        }
        reasonWhyNot = "";
        return true;
    }

    public void Update()
    {
        if (SelectedMission == null)
        {
            Title.text = "Select a mission.";
            Description.text = "";
            return;
        }

        string reasonInadmissable;
        if (ShouldBeAbleToGoOnMission(out reasonInadmissable))
        {
            Title.text = SelectedMission.Name;
            EmbarkButton.interactable = true;
        }
        else
        {
            Title.text = SelectedMission.Name + $"\n<color=red>[Unable to embark; {reasonInadmissable}]</color>";
            EmbarkButton.interactable = false;
        }

        Description.text = SelectedMission.GenerateMissionDescriptiveText();

        if (SelectedMission?.Terrain?.TerrainImage != null)
        {
            terrainImage.sprite = SelectedMission.Terrain.TerrainImage.ToSprite();
        }
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

            GameState.Instance.AllyUnitsSentOnRun = GetCharactersSent();
            GameScenes.SwitchToBattleScene(selectedMission, charactersSent);
        });
    }

    private List<AbstractBattleUnit> GetCharactersSent()
    {
        return RosterPrefab.Instance.GetCharactersSelected().ToList();
    }

    private AbstractMission GetMissionSelected()
    {
        return SelectedMission;
    }
}
