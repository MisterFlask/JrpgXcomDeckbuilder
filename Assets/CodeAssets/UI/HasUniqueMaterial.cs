using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI
{
    public class HasUniqueMaterial : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            if (GetComponent<Image>() != null)
            {
                GetComponent<Image>().material = Instantiate<Material>(GetComponent<Image>().material);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}