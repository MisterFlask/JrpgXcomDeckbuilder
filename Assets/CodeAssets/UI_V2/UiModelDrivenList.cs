using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeAssets.UI_V2
{
    public class UiModelDrivenList<T>: MonoBehaviour
    {
        // The prefab for the UI element to instantiate for each model object
        public GameObject uiElementPrefab;

        // The list of model objects to display
        public List<T> modelObjects;

        // The list of UI elements currently displayed
        private List<UiModelDrivenListElement<T>> uiElements = new List<UiModelDrivenListElement<T>>();

        // Update is called once per frame
        void Update()
        {
            // First, remove any UI elements that no longer have corresponding model objects
            for (int i = uiElements.Count - 1; i >= 0; i--)
            {
                if (!modelObjects.Contains(uiElements[i].GetComponent<UiModelDrivenListElement<T>>().ModelObject))
                {
                    Destroy(uiElements[i]);
                    uiElements.RemoveAt(i);
                }
            }

            // Next, add UI elements for any new model objects
            foreach (T modelObject in modelObjects)
            {
                if (!uiElements.Any(uiElement => uiElement.ModelObject.Equals(modelObject)))
                {
                    GameObject uiElement = Instantiate(uiElementPrefab, transform);
                    uiElement.GetComponent<UiModelDrivenListElement<T>>().ModelObject = modelObject;
                    uiElements.Add(uiElement.GetComponent<UiModelDrivenListElement<T>>());
                }
            }
        }
    }
}
