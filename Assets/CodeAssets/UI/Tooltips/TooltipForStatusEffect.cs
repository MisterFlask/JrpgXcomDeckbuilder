using ModelShark;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.CodeAssets.UI.Tooltips
{
    [RequireComponent(typeof(TooltipTriggerController))]
    public class TooltipForStatusEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public BattleUnitAttributePrefab StatusEffectPrefab;
        public TooltipTriggerController TriggerController => GetComponent<TooltipTriggerController>();

        public void OnPointerEnter(PointerEventData eventData)
        {
            TriggerController.ShowTooltipForStatusEffect(StatusEffectPrefab.CorrespondingAttribute);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}