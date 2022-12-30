using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeAssets.StsMapScreen
{
    public class ShopContextualHider: MonoBehaviour
    {

        public GameObject ShopButton;

        public void Update()
        {
            if (GameState.Instance.CurrentMapNode is ShopRichMapNode)
            {
                this.ShopButton.SetActive(true) ;
            }
            else
            {
                this.ShopButton.SetActive(false) ;
            }
        }
    }
}
