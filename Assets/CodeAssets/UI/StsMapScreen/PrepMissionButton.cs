using Map;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.UI.StsMapScreen
{
    public class PrepMissionButton : MonoBehaviour
    {


        public TMPro.TextMeshProUGUI ButtonText;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClick()
        {
            if (GameState.Instance.AlreadyExploredNode)
            {
                Debug.Log("Already explored node");
                return;
            }

            var currentNode = GameState.Instance.CurrentMapNode;

            if (currentNode.Node.nodeType.IsCombatNode())
            {
                // set current mission to this mission
                // switch to campaign screen
            }
        }
    }
}