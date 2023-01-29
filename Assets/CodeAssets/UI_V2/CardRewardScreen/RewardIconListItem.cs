using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI_V2
{
    public class RewardIconListItem: MonoBehaviour
    {
        public Image ItemIcon;
        public TextMeshProUGUI ItemText;

        public AbstractMissionReward Reward;
        
        public void SetTextAndIcon(ProtoGameSprite sprite, string text)
        {
            ItemText.text = text;
            ItemIcon.sprite = sprite.ToGameSpriteImage().Sprite;
        }

        public void OnSelect()
        {
            if (Reward is RandomCardReward)
            {
                var reward = (RandomCardReward)Reward;
                FindObjectOfType<CardRewardScreen>().Show(reward.Soldier.SoldierClass.GetCardRewardsForLevel(1, 3) , reward.Soldier);
            }else
            {
                Reward.OnReward();
            }
        }
    }
}
