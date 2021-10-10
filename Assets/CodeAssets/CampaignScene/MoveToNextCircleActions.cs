using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeAssets.CampaignScene
{
    public class MoveToNextCircleActions : MonoBehaviour
    {

        public Button moveToNextCircleButton;

        public void MoveToNextCircle()
        {
            // do the things required to get to the next circle.
            //todo
            Debug.Log("Haven't implemented this part yet");

        }

        public void Update()
        {
            if (!GameState.Instance.NextRegionUnlocked)
            {
                moveToNextCircleButton.interactable = false;
            }
            else
            {
                moveToNextCircleButton.interactable = true;
            }
        }
    }
}