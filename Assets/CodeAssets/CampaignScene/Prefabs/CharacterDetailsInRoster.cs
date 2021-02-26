using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterDetailsInRoster : MonoBehaviour
{
    public AbstractBattleUnit BattleUnit { get; set; }

    public Image CharacterImage;
    public TMPro.TextMeshProUGUI Title;
    public TMPro.TextMeshProUGUI Description;

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
            if (!BattleUnit.IsDead)
            {
                Title.text = BattleUnit.CharacterFullName;
            }
            else
            {
                Title.text = BattleUnit.CharacterFullName + "[DEAD]";
            }
        }
    }

}
