using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbstractBattleUnit
{
    public string ImageString { get; set; } = ImageUtils.StockPlaceholderImage;

    public int MaxHp { get; set; }

    public int CurrentHp { get; set; }

    public string Name { get; set; }

    public List<AbstractBattleUnitAttribute> Attributes { get; set; }

    public BattleUnitPrefab CorrespondingPrefab { get; set; }
}
