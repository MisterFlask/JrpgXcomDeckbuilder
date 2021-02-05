using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI.Subscreens
{
    public class AssignAugmentationDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Image AugmentationImage;
        public Button AssignmentButton;

        private bool IsTaken { get; set; } = false;

        private AbstractBattleUnit BattleUnitSelected => GameState.Instance.CharacterSelected;
        public AbstractAugmentation Augmentation { get; set; }
        public AbstractBattleUnit SoldierWeAreConsideringAssigningThisTo { get; set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Augmentation == null)
            {
                return;
            }
            ExplainerPanel.ShowAugmentationHelp(Augmentation);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ExplainerPanel.Hide();
        }

        // Use this for initialization
        void Start()
        {
            AssignmentButton.onClick.AddListener(() =>
            {
                SoldierWeAreConsideringAssigningThisTo.ApplyAugmentation(Augmentation);
            });
        }

        // Update is called once per frame
        void Update()
        {
            AugmentationImage.SetProtoSprite(Augmentation.ProtoSprite);
        }
    }
}