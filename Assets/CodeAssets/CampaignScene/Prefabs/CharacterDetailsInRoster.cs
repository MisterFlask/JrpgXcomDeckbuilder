using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterDetailsInRoster : MonoBehaviour
{
    public AbstractBattleUnit BattleUnit { get; set; }

    public Image CharacterImage;
    public Button ShowDeckButton;
    public TMPro.TextMeshProUGUI Title;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (BattleUnit != null)
        {
            CharacterImage.sprite = BattleUnit.ProtoSprite.ToGameSpriteImage().Sprite;
            Title.text = BattleUnit.CharacterName;
        }
    }
}
