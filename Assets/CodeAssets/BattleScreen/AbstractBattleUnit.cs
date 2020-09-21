using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEditor;

public abstract class AbstractBattleUnit
{
    public string Guid = UnityEditor.GUID.Generate().ToString();
    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon();

    public bool HasStatusEffect<T>() where T:AbstractStatusEffect
    {
        return Attributes.Any(item => item.GetType() == typeof(T));
    }

    public int MaxHp { get; set; }

    public bool IsDead => CurrentHp == 0;

    public int CurrentDefense { get; set; }
    public int CurrentHp { get; set; }
    public int CurrentFatigue { get; set; } = 4;
    public int MaxFatigue { get; set; } = 4;

    public string UnitClassName { get; set; }
    public string CharacterName { get; set; } = "";

    public List<AbstractStatusEffect> Attributes { get; set; } = new List<AbstractStatusEffect>();

    public BattleUnitPrefab CorrespondingPrefab { get; set; }

    public bool IsAlly { get; set; }

    public bool IsEnemy { get => !IsAlly;  }

    // Expected to be empty for enemies
    public List<AbstractCard> StartingCardsInDeck { get; set; } = new List<AbstractCard>();

    private List<AbstractCard> _CardsInPersistentDeck { get; set; } = new List<AbstractCard>();

    public IEnumerable<AbstractCard> CardsInPersistentDeck => _CardsInPersistentDeck;

    public List<AbstractCard> BattleDeck { get; set; } = new List<AbstractCard>();

    public void ResetPersistentDeck()
    {
        _CardsInPersistentDeck = new List<AbstractCard>();
        AddCardsToPersistentDeck(StartingCardsInDeck);
    }

    public void AddCardsToPersistentDeck(IEnumerable<AbstractCard> cards)
    {
        foreach(var baseCard in cards)
        {
            AddCardToPersistentDeck(baseCard);
        }
    }

    private void AddCardToPersistentDeck(AbstractCard baseCard)
    {
        var card = baseCard.CopyCard();
        card.Owner = this;
        _CardsInPersistentDeck.Add(card);
    }

    public abstract List<Intent> GetNextIntents();

    public bool IsAiControlled = true;

    public List<Intent> CurrentIntents = new List<Intent>();
    
    public int Turn { get; set; } = 1;

    public bool IsAdvanced { get; set; } = false;

    public void Die()
    {
        CurrentHp = 0;
    }

    public void OnTurnStart()
    {
        CurrentDefense = 0;
    }

    public void ExecuteOnIntentIfAvailable()
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
                foreach (var intent in CurrentIntents)
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
        BattleDeck = new List<AbstractCard>();
        BattleDeck.AddRange(CardsInPersistentDeck);
    }

    public AbstractBattleUnit InitPersistentUnitFromTemplate()
    {
        var copy = (AbstractBattleUnit)this.MemberwiseClone();
        copy.Guid = GUID.Generate().ToString();
        copy.ResetPersistentDeck();
        var newName = CharacterNameGenerator.GenerateCharacterName();
        copy.CurrentFatigue = MaxFatigue;
        copy.CurrentHp = MaxHp;
        copy.CharacterName =  newName.Nickname;
        return copy;
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
