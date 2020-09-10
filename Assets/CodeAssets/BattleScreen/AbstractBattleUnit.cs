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

    public List<AbstractStatusEffect> Attributes { get; set; } = new List<AbstractStatusEffect>();

    public BattleUnitPrefab CorrespondingPrefab { get; set; }

    public bool IsAlly { get; set; }

    public bool IsEnemy { get => !IsAlly;  }

    // Expected to be empty for enemies
    public List<AbstractCard> CardsInDeck { get; set; } = new List<AbstractCard>();

    public abstract List<Intent> GetNextIntents();

    public bool IsAiControlled = true;

    public List<Intent> CurrentIntents = new List<Intent>();
    
    public int Turn { get; set; } = 1;

    public void Die()
    {
        CurrentHp = 0;
    }

    public void OnTurnStart()
    {
        if (IsDead)
        {
            CurrentIntents = null;
            return;
        }

        Turn++;
        if (IsAiControlled)
        {
            if (CurrentIntents != null)
            {
                foreach(var intent in CurrentIntents)
                {
                    intent.ExecuteIntent();
                }
            }
            CurrentIntents = GetNextIntents();
        }
    }

    public void InitForBattle()
    {
        this.CurrentFatigue = MaxFatigue;
        if (IsEnemy)
        {
            this.CurrentHp = MaxHp;
        }
        this.CurrentIntents = GetNextIntents();
    }

    #region convenience functions
    protected ActionManager action()
    {
        return ServiceLocator.GetActionManager();
    }

    protected List<AbstractBattleUnit> enemies()
    {
        return ServiceLocator.GetGameStateTracker().EnemyUnitsInBattle;
    }
    protected List<AbstractBattleUnit> allies()
    {
        return ServiceLocator.GetGameStateTracker().AllyUnitsInBattle;
    }

    protected GameState state()
    {
        return ServiceLocator.GetGameStateTracker();
    }
    #endregion

}
