using ModelShark;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.CodeAssets.UI.Tooltips
{
    [RequireComponent(typeof(TooltipTriggerController))]
    public class TooltipForSelectedCharacter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TooltipTriggerController TriggerController => GetComponent<TooltipTriggerController>();

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (GameState.Instance.CharacterSelected == null)
            {
                TriggerController.CardTooltipTrigger.enabled = false;
            }
            else
            {
                TriggerController.CardTooltipTrigger.enabled = true;
                TriggerController.ShowTooltipForBattleUnitClass(GameState.Instance.CharacterSelected);
            }
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