﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableMissionPrefab : MonoBehaviour, IPointerClickHandler
{
    public TMPro.TextMeshProUGUI Title;
    public Image MissionImage;

    public Mission Mission { get; set; }
    public static SelectableMissionPrefab CurrentlySelected = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        CurrentlySelected = this;
    }

    // Use this for initialization
    void Start()
    {
        MissionImage.sprite = ImageUtils.ProtoGameSpriteFromGameIcon().ToGameSpriteImage().Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        Title.text = Mission.Name;
        if (CurrentlySelected == this)
        {
            Title.color = Color.yellow;
        }
        else
        {
            Title.color = Color.black;
        }
    }
}