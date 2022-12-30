using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeAssets.StsMapScreen
{
    public class MissionLaunchContextualHider: MonoBehaviour
    {
        public GameObject Button;

        public void Update()
        {
            if (GameState.GetCurrentNodeContents() == null)
            {
                return;
            }
            if (GameState.GetCurrentNodeContents().nodeType() == Map.NodeType.MinorEnemy
                || GameState.GetCurrentNodeContents().nodeType() == Map.NodeType.EliteEnemy)
            {
                this.Button.SetActive(true) ;
            }
            else
            {
                this.Button.SetActive(false) ;
            }
        }
    }
}
