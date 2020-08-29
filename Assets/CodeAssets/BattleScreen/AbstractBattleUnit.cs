using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractBattleUnit
{
    public string Guid = UnityEditor.GUID.Generate().ToString();
    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoSpriteFromGameIcon();

    public int MaxHp { get; set; }

    public bool IsDead => CurrentHp == 0;

    public int CurrentHp { get; set; }

    public string Name { get; set; }

    public List<AbstractBattleUnitAttribute> Attributes { get; set; }

    public BattleUnitPrefab CorrespondingPrefab { get; set; }

    public bool IsAlly { get; set; }

    public bool IsEnemy { get => !IsAlly;  }

    // Expected to be empty for enemies
    public List<AbstractCard> CardsInDeck { get; set; } = new List<AbstractCard>();

    public abstract Intent GetNextIntent();

    public bool IsAiControlled = true;

    public Intent CurrentIntent = null;

    public int Turn { get; set; } = 1;

    public void OnTurnStart()
    {
        Turn++;
        if (IsAiControlled)
        {
            if (CurrentIntent != null)
            {
                CurrentIntent.Execute();
            }
            CurrentIntent = GetNextIntent();
        }
    }
}
