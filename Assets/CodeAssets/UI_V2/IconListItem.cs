using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI_V2
{
    public class IconListItem
    {
        public Image ItemIcon;
        public TextMeshProUGUI ItemText;

        public void SetTextAndIcon(ProtoGameSprite sprite, string text)
        {
            ItemText.text = text;
            ItemIcon.sprite = sprite.ToGameSpriteImage().Sprite;
        }
    }
}
