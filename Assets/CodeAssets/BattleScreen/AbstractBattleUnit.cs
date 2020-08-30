using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractBattleUnit
{
    public string Guid = UnityEditor.GUID.Generate().ToString();
    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon();

    public int MaxHp { get; set; }

    public bool IsDead => CurrentHp == 0;

    public int CurrentHp { get; set; }
    public int CurrentFatigue { get; set; } = 4;
    public int MaxFatigue { get; set; } = 4;

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

    public void Die()
    {
        CurrentHp = 0;
    }

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

    public void InitForBattle()
    {
        this.CurrentFatigue = MaxFatigue;
        if (IsEnemy)
        {
            this.CurrentHp = MaxHp;
        }
    }

    #region convenience functions
    public ActionManager action()
    {
        return ServiceLocator.GetActionManager();
    }

    public List<AbstractBattleUnit> enemies()
    {
        return ServiceLocator.GetGameStateTracker().EnemyUnitsInBattle;
    }
    public List<AbstractBattleUnit> allies()
    {
        return ServiceLocator.GetGameStateTracker().AllyUnitsInBattle;
    }

    public GameState state()
    {
        return ServiceLocator.GetGameStateTracker();
    }
    #endregion

}
