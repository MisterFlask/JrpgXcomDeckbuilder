using UnityEngine;
using System.Collections;

public class ActAndDayPanel : MonoBehaviour
{
    public TMPro.TextMeshProUGUI DayAndActDisplay;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var day = GameState.Instance.Day;
        DayAndActDisplay.text = $"Day {day}";
    }
}
