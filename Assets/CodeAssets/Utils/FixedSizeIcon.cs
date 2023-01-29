using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeAssets.Utils
{
    public class FixedSizeIcon: MonoBehaviour
    {
        public SpriteRenderer Renderer;
        float originalSizeX { get; set; }
        float originalSizeY { get; set; }
        public void Start()
        {
            originalSizeX = Renderer.localBounds.size.x;
            originalSizeY = Renderer.localBounds.size.y;
        }

        public void SetIcon(Sprite sprite)
        {
            ProtoGameSprite.SetSpriteRendererToSpriteWhileMaintainingSize(originalSizeX, originalSizeY, sprite, Renderer);
        }
        
    }
}
