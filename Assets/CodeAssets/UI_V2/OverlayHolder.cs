using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeAssets.UI_V2
{
    /// <summary>
    ///  Required because we want the overlays to start off disabled.
    /// </summary>
    public class OverlayHolder: MonoBehaviour
    {
        public static OverlayHolder Instance => GameObject.FindObjectOfType<OverlayHolder>();

        public GameObject MapStatusOverlay;
        public GameObject ShopOverlay;
        public GameObject RestOverlay;
        public ShowDeckScreen ShowDeckScreen;

        public void Start()
        {
            ShowDeckScreen.Instance = ShowDeckScreen;
        }
        public void SetShowStatus(OverlayType type, bool show)
        {
            switch (type)
            {
                case OverlayType.MapStatus:
                    OverlayHolder.Instance.MapStatusOverlay.SetActive(show);
                    RosterPrefab.Instance.Initialize();
                    break;
                case OverlayType.Shop:
                    OverlayHolder.Instance.ShopOverlay.SetActive(show);
                    break;
                case OverlayType.Rest:
                    OverlayHolder.Instance.RestOverlay.SetActive(show);
                    break;
                case OverlayType.ShowDeckScreen:
                    OverlayHolder.Instance.ShowDeckScreen.gameObject.SetActive(show);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        public static void SetShowStatusOfOverlay(OverlayType type, bool show)
        {
            Instance.SetShowStatus(type, show);
        }
    }
    
    public enum OverlayType
    {
        MapStatus,
        Shop,
        Rest,
        ShowDeckScreen
    }
    
}
