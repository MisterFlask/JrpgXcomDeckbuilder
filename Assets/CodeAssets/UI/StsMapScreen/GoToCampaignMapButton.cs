using Map;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.UI.StsMapScreen
{
    public class GoToCampaignMapButton : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClick()
        {
            GameScenes.StsMapScene();
        }
    }
}