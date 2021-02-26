using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectedCharacterDisplay : MonoBehaviour
{

    public TMPro.TextMeshProUGUI Text;
    public Image image;

    public AbstractBattleUnit SelectedCharacter => GameState.Instance.CharacterSelected;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedCharacter == null)
        {
            return;
        }
        var deceased = SelectedCharacter.IsDead;
        image.SetProtoSprite(SelectedCharacter.ProtoSprite);
        if (deceased)
        {
            Text.text = $"{SelectedCharacter.CharacterFullName} [DEAD]";
        }
        else
        {
            Text.text = $"{SelectedCharacter.CharacterFullName} [{SelectedCharacter.UnitClassName}]";
        }
    }
}
