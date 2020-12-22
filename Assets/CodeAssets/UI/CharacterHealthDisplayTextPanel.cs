using UnityEngine;
using System.Collections;
using System;

public class CharacterHealthDisplayTextPanel : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Text;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var characterSelected = GameState.Instance.CharacterSelected;
        if (characterSelected == null)
        {
            return;
        }
        Text.text = $"HP: {characterSelected.CurrentHp}/{characterSelected.MaxHp} [Heals at {characterSelected.PerDayHealingRate}]";
        Text.text += Environment.NewLine + $"Stress: {characterSelected.CurrentStress}/{characterSelected.MaxStress} [Heals at {characterSelected.PerDayStressHealingRate}]";
    }
}
