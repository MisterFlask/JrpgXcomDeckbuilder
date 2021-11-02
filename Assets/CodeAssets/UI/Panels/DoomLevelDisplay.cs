using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.UI.Panels
{
    public class DoomLevelDisplay : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI Text;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Text.SetText(GameState.Instance.DoomCounter.GetCurrentDoomLevel().Description);
        }
    }
}