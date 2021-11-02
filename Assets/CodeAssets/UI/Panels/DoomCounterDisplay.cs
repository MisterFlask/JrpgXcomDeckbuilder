using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.UI.Panels
{
    public class DoomCounterDisplay : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI Text;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Text.SetText("Doom Level: " + GameState.Instance.DoomCounter.CurrentDoomCounter + "");
        }
    }
}