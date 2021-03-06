﻿using ModelShark;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.UI
{
    /// <summary>
    /// Just initializes our custom tooltips.
    /// </summary>
    public class CustomTooltipManager : MonoBehaviour
    {

        public TooltipStyle CardTooltipStyle;

        public static TooltipStyle STYLE_INSTANCE => INSTANCE.CardTooltipStyle;

        private static CustomTooltipManager INSTANCE;

        public CustomTooltipManager()
        {
            INSTANCE = this;
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}