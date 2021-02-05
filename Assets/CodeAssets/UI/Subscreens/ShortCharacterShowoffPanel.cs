using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI.Subscreens
{
    public class ShortCharacterShowoffPanel : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI CharacterNameText;
        public Image Image;

        public AbstractBattleUnit Character;

        // Use this for initialization
        void Start()
        {
            CharacterNameText.text = Character.GetDisplayName(DisplayNameType.FULL_NAME);
            Image.SetProtoSprite(Character.ProtoSprite);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}