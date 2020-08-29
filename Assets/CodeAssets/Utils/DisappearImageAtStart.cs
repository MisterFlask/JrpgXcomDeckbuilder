using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DisappearImageAtStart : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
