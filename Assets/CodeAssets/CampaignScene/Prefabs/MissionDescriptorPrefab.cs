using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

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
     }

    public void Start()
    {
        EmbarkButton.onClick.AddListener(() =>
        {
            // todo: Figure out scene transition.
            // Set up static stuff for combat.
        });

        
    }
}
