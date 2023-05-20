using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Assets.CodeAssets.UI_V2
{
    public class StopClickPropagation : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

}
