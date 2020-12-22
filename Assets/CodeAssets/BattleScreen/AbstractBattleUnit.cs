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

    public bool HasStatusEffect<T>() where T : AbstractStatusEffect
    {
        return StatusEffects.Any(item => item.GetType() == typeof(T));
    }

    public int CombatsParticipatedIn = 0;
    public bool PromotionAvailable => this.SoldierClass is RookieClass && CombatsParticipatedIn > 0;

    public int MaxHp { get; set; }
    public int NumberCardRewardsEligibleFor { get; set; } = 0;


    public bool IsDead => CurrentHp <= 0;
    public int CurrentLevel { get; set; } = 1;
    public int CurrentBlock { get; set; }
    public int CurrentHp { get; set; }
    public int PerDayHealingRate { get; set; } = 2;

    public int CurrentStress => this.GetStatusEffect<StressStatusEffect>()?.Stacks ?? 0;
    public int MaxStress = 100;
    public int PerDayStressHealingRate = 5;

    public int CurrentFatigue { get; set; } = 4;
    public int MaxFatigue { get; set; } = 4;


    public AbstractSoldierClass SoldierClass { get; protected set; } = new RookieClass();
    public string UnitClassName => SoldierClass.Name();
    public string CharacterName { get; set; } = "";

    public List<AbstractStatusEffect> StatusEffects { get; set; } = new List<AbstractStatusEffect>();

    public BattleUnitPrefab CorrespondingPrefab { get; set; }

    public bool IsAlly { get; set; }

    public bool IsEnemy { get => !IsAlly;  }

    // Expected to be empty for enemies
    public List<AbstractCard> StartingCardsInDeck { get; set; } = new List<AbstractCard>();

    private List<AbstractCard> _CardsInPersistentDeck { get; set; } = new List<AbstractCard>();

    public IEnumerable<AbstractCard> CardsInPersistentDeck => _CardsInPersistentDeck;

    public void RemoveCardsFromPersistentDeck(IEnumerable<AbstractCard> cardsToRemove)
    {
        _CardsInPersistentDeck.RemoveAll(item => cardsToRemove.Contains(item));

    }
    public void RemoveCardsFromPersistentDeckByType<T>() where T:AbstractCard
    {
        _CardsInPersistentDeck.RemoveAll(item => item.GetType() == typeof(T));

    }

    public List<AbstractCard> BattleDeck { get; set; } = new List<AbstractCard>();

    public List<SoldierPerk> Perks { get; } = new List<SoldierPerk>();

    public void InitializePersistentDeck()
    {
        _CardsInPersistentDeck = new List<AbstractCard>();
        AddCardsToPersistentDeck(StartingCardsInDeck);
    }

    public void AddStatusEffect<T>(T effect, int stacks = 1) where T:AbstractStatusEffect
    {
        if (HasStatusEffect<T>() && !GetStatusEffect<T>().Stackable)
        {
            return;
        }

        if (HasStatusEffect<T>())
        {
            GetStatusEffect<T>().Stacks += stacks;
            if (GetStatusEffect<T>().Stacks < 0 && !GetStatusEffect<T>().AllowedToGoNegative)
            {
                RemoveStatusEffect<T>();
            }
            else
            {
                if (stacks > 0)
                {
                    BattleRules.ProcessHooksWhenStatusEffectAppliedToUnit(this, effect, stacks);
                }
            }
        }
        else
        {
            effect.AssignOwner(this);
            effect.Stacks = stacks;
            StatusEffects.Add(effect);
            BattleRules.ProcessHooksWhenStatusEffectAppliedToUnit(this, effect, stacks);
        }
    }

    public AbstractStatusEffect GetStatusEffect<T>()
    {
        return this.StatusEffects.Where(item => item is T).FirstOrDefault();
    }

    public void AddCardsToPersistentDeck(IEnumerable<AbstractCard> cards)
    {
        foreach(var baseCard in cards)
        {
            AddCardToPersistentDeck(baseCard);
        }
    }

    public void AddCardToPersistentDeck(AbstractCard baseCard)
    {
        var card = baseCard.CopyCard();
        card.Owner = this;
        _CardsInPersistentDeck.Add(card);
    }

    public abstract List<AbstractIntent> GetNextIntents();

    public bool IsAiControlled = true;

    public List<AbstractIntent> CurrentIntents = new List<AbstractIntent>();
    
    public int Turn { get; set; } = 1;

    public bool IsAdvanced => HasStatusEffect<AdvancedStatusEffect>();

    public void Die()
    {
        CurrentHp = 0;
    }

    public void OnTurnStart()
    {
        ActionManager.Instance.DoAThing(() =>
        {
            CurrentBlock = 0;
            if (CurrentFatigue < MaxFatigue)
            {
                this.CurrentFatigue += 1;
            }
            foreach(var statusEffect in StatusEffects)
            {
                statusEffect.OnTurnStart();
            }
        });
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

    public AbstractBattleUnit CloneUnit()
    {
        var copy = (AbstractBattleUnit)this.MemberwiseClone();
        copy.Guid = GUID.Generate().ToString();
        copy.InitializePersistentDeck();
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
    protected List<AbstractBattleUnit> allAlliedUnits()
    {
        return ServiceLocator.GetGameStateTracker().AllyUnitsInBattle;
    }

    protected GameState state()
    {
        return ServiceLocator.GetGameStateTracker();
    }

    /// <summary>
    /// This is just a cleanup operation.
    /// </summary>
    public void PerformStateBasedActions()
    {
        foreach(var effect in StatusEffects.ToList())
        {
            if (effect.Stacks == 0)
            {
                StatusEffects.Remove(effect);
            }
        }
        BattleRules.CheckAndRegisterDeath(this, null);

        // kind of a hack, but should be fine
        foreach(var statusEffect in StatusEffects.ToList())
        {
            if (!StatusEffects.Contains(statusEffect))
            {
                continue;
            }

            var effectsOfThisType = StatusEffects.Where(item => item.GetType() == statusEffect.GetType());
            if (effectsOfThisType.Count() > 1)
            {
                var consolidatedStacks = effectsOfThisType.Sum(item => item.Stacks);
                StatusEffects.RemoveAll(item => item.GetType() == statusEffect.GetType());
                StatusEffects.Add(statusEffect);
                statusEffect.Stacks = consolidatedStacks;
            }
        }

    }



    /// <summary>
    /// If stacksToRemove is null, removes all stacks.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stacksToRemove"></param>
    public void RemoveStatusEffect<T>(int? stacksToRemove = null) where T:AbstractStatusEffect
    {
        if (this.HasStatusEffect<T>())
        {
            var attribute = this.StatusEffects.First(item => item is T);
            if (stacksToRemove == null)
            {
                this.StatusEffects.Remove(attribute);
            }
            else
            {
                attribute.Stacks -= stacksToRemove ?? 0;
            }
        }
    }

    public void ApplySoldierPerk(SoldierPerk perk, int stacks = 1)
    {
        perk.Stacks = stacks;
        Perks.Add(perk);
    }
    public void RemoveSoldierPerk<T>() where T : SoldierPerk
    {
        Perks.RemoveAll(item => item.GetType() == typeof(T));
    }

    public void RemoveSoldierPerkByType(Type t)
    {
        Perks.RemoveAll(item => item.GetType() == t);
    }

    public void ChangeClass(AbstractSoldierClass newClass)
    {
        this.SoldierClass = newClass;
    }

    private bool difficultyInitialized = false;
    public virtual void SetDifficulty(int difficulty)
    {
        if (difficultyInitialized)
        {
            throw new Exception("Initialized difficulty twice!");
        }
        difficultyInitialized = true;
        AddStatusEffect(new PowerStatusEffect(), difficulty);
    }
    #endregion


    public BattleUnitStatisticsInThisCombat StatsForThisCombat = new BattleUnitStatisticsInThisCombat();

    internal void LevelUp()
    {
        CurrentLevel ++;
        NumberCardRewardsEligibleFor ++;
        CurrentHp += 2;

    }
}

public class BattleUnitStatisticsInThisCombat
{
    int AmountOfDamageTaken = 0;
    int NumberOfTimesStruck = 0;
}