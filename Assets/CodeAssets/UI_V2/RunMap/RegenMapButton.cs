using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI_V2.RunMap
{
    public class RegenMapButton: MonoBehaviour
    {

        public Button button;

        void Start()
        {
            button.onClick.AddListener(HandleClick);
        }

        void HandleClick()
        {
            Debug.Log("CLicked regen map button");
            var mapUi = MapUI.INSTANCE;
            mapUi.ModelObject = mapUi.RegenerateInitialModel();

        }

    }
}
