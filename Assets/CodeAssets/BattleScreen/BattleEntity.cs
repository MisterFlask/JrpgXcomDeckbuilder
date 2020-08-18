using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleEntity : MonoBehaviour
{
    public string ImageString { get; set; } = ImageUtils.StockPlaceholderImage;

    public int MaxHp { get; set; }

    public int CurrentHp { get; set; }

    public string Name { get; set; }

    public List<BattleUnitAttribute> Attributes { get; set; }

    public void OnDeath()
    {

    }

}
