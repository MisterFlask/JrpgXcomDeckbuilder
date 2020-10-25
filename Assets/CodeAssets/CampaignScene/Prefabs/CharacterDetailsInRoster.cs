using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterDetailsInRoster : MonoBehaviour
{
    public AbstractBattleUnit BattleUnit { get; set; }

    public Image CharacterImage;
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
            CharacterImage.SetProtoSprite(BattleUnit.ProtoSprite);
            Title.text = BattleUnit.CharacterName;
        }
    }
}
