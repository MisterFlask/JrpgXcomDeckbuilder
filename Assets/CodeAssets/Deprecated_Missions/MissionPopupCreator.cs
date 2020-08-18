using UnityEngine;
using System.Collections;

public class MissionPopupCreator : MonoBehaviour
{
    public void CreatePopup(ClickableTile tile) 
    {
        var popupTemplate = GameObject.Find("EVENT_NOTIFICATION_TEMPLATE");
        var newPopup = GameObject.Instantiate(popupTemplate, tile.transform.parent); // parent this to the tilemap
        newPopup.transform.position = tile.transform.position;
    }
}
