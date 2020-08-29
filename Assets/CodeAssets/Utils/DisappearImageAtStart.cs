﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DisappearImageAtStart : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Image>().color = Color.white.WithAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}