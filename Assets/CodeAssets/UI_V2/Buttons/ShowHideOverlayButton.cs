using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeAssets.UI_V2.Buttons
{
    public class ShowHideOverlayButton: MonoBehaviour
    {
        public OverlayType Overlay;
        public bool Show;

        public void OnClick()
        {
            OverlayHolder.SetShowStatusOfOverlay(Overlay, Show);
        }
    }
}
