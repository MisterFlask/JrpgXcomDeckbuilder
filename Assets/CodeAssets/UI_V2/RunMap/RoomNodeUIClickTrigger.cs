using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeAssets.UI_V2.RunMap
{
    public class RoomNodeUIClickTrigger: MonoBehaviour
    {
        public RoomNodeUI ParentNode;

        void OnMouseDown()
        {
            Debug.Log("Clicked ui trigger for room node");
            ParentNode.MouseDownClickHandler();
        }
    }
}
