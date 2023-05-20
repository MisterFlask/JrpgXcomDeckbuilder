using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.CodeAssets.UI_V2.RunMap
{
    public class RoomNodeUIClickTrigger: MonoBehaviour, IPointerClickHandler
    {
        public RoomNodeUI ParentNode;

        public void OnPointerClick(PointerEventData eventData)
        {
            ParentNode.MouseDownClickHandler();
        }
    }
}
