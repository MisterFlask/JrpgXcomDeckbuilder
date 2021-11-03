using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.UI
{
    public class BattleRewardDisplay : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI RewardText;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GameState.Instance.CurrentMission != null)
            {
                var text = "";

                if (GameState.Instance.CurrentMission.IsVictory)
                {
                    foreach (var reward in GameState.Instance.CurrentMission.Rewards)
                    {
                        text = text + reward.GenericDescription() + "\n";
                    }
                }

                RewardText.text = text; 
            }
        }
    }
}