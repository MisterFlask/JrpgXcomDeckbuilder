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
            Title.text = BattleUnit.CharacterName;
        }
    }

    string CharacterDescription()
    {
        if (BattleUnit.IsDead)
        {
            return $"{BattleUnit.SoldierClass.Name()}\n<color=red>Deceased.</color>";
        }

        return $"{BattleUnit.SoldierClass.Name()}\nHP:{BattleUnit.CurrentHp}/{BattleUnit.MaxHp}\nStress:0";
    }
}
