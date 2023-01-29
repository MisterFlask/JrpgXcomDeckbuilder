using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeAssets.UI_V2
{
    public class RewardListPrefab: MonoBehaviour
    {
        public RewardIconListItem ListItemPrefab;
        public GameObject ListParent;
        
        public void SetRewards(List<AbstractMissionReward> Rewards)
        {

            ClearChildren(ListParent.transform);
            foreach (var item in Rewards)
            {
                var newTransform = GameObject.Instantiate(ListItemPrefab, ListParent.transform);
                newTransform.SetTextAndIcon(item.ProtoSprite, item.GetSpecificDescription());
            }
        }

        public void ClearChildren(Transform transform)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
