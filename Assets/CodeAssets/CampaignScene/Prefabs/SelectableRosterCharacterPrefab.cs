﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SelectableRosterCharacterPrefab : MonoBehaviour, IPointerClickHandler
{
    public TMPro.TextMeshProUGUI Title;
    public Image MissionImage;
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
        MissionImage.sprite = ImageUtils.ProtoGameSpriteFromGameIcon().ToGameSpriteImage().Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        Title.text = Character.CharacterName;
    }
}