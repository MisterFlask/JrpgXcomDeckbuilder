using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class SelectableMissionPrefab : MonoBehaviour, IPointerClickHandler
{
    public Guid guid = Guid.NewGuid();

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
        MissionImage.SetProtoSprite(ImageUtils.ProtoGameSpriteFromGameIcon());
    }

    // Update is called once per frame
    void Update()
    {
        if (Mission == null)
        {
            return;
        }
        Title.text = Mission.Name + $"[{Mission.DaysUntilExpiration} days remain]";
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
