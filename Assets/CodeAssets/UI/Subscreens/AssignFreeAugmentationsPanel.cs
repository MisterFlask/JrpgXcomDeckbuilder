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
        }
        public void Awake()
        {
            Instance = this;
        }

        public AssignAugmentationDisplay AugmentationDisplayTemplate;
        public Transform AugmentationDisplayParent;


        // Update is called once per frame
        void Update()
        {

        }

        public void Show()
        {
            Init(GameState.Instance.AugmentationInventory);
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void Init(List<AbstractSoldierPerk> augmentationsToDisplay)
        {
            AugmentationDisplayParent.gameObject.PurgeChildren();

            foreach(var augmentation in augmentationsToDisplay)
            {
                var newDisplay = AugmentationDisplayTemplate.Spawn(AugmentationDisplayParent);
                newDisplay.Augmentation = augmentation;
                newDisplay.Init();
            }
        }
    }
}