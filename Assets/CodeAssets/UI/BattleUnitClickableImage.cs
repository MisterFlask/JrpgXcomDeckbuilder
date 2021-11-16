using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.UI
{
    public class BattleUnitClickableImage : MonoBehaviour
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

        public void OnMouseDown()
        {
            linkedTo.OnPointerClick(null);
        }

        public void OnMouseEnter()
        {
            linkedTo.OnPointerEnter(null);
        }

        public void OnMouseExit()
        {
            linkedTo.OnPointerExit(null);
        }
    }
}