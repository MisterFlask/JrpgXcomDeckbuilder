using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.UI.Subscreens
{
    public class AssignFreeAugmentationsPanel : MonoBehaviour
    {

        public static AssignFreeAugmentationsPanel Instance;
        public AssignFreeAugmentationsPanel()
        {
            Instance = this;
        }


        public AssignAugmentationDisplay AugmentationDisplayTemplate;
        public Transform AugmentationDisplayParent;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void Init(List<AbstractAugmentation> augmentationsToDisplay)
        {
            AugmentationDisplayParent.gameObject.PurgeChildren();

            foreach(var augmentation in augmentationsToDisplay)
            {
                var newDisplay = AugmentationDisplayTemplate.Spawn(AugmentationDisplayParent);
                newDisplay.Augmentation = augmentation;
                newDisplay.SoldierWeAreConsideringAssigningThisTo = GameState.Instance.CharacterSelected;
                
            }
        }
    }
}