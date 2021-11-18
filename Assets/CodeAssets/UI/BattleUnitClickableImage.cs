using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.CodeAssets.UI
{
    public class BattleUnitClickableImage : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public BattleUnitPrefab linkedTo;

        private void Awake()
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

        public SpriteRenderer GetSpriteRenderer()
        {
            return GetComponent<SpriteRenderer>();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            linkedTo.OnPrefabClick(null);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            linkedTo.OnPrefabEnter(null);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            linkedTo.OnPrefabExit(null);
        }
    }
}