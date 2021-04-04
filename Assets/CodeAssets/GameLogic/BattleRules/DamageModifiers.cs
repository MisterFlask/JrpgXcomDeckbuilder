using Assets.CodeAssets.BattleEntities.StatusEffects;
using Assets.CodeAssets.Cards;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public abstract class DamageModifier
{
    public bool IgnoresDefense { get; set; } = false;

    public bool IgnoresRetaliation { get; set; } = false;

    /// <summary>
    /// If true, this will get incorporated in damage calcs when just looking at the card.
    /// </summary>
    public bool TargetInvariant { get; set; } = false;

    public string Name { get; set; } = "Sample";

    public string Description { get; set; } = "Tooltip Text";

    public virtual void Slay(AbstractCard damageSource, AbstractBattleUnit target)
    {

    }

    public virtual void OnStrike(AbstractCard damageSource, AbstractBattleUnit target, int totalDamageAfterModifiers)
    {
    }

    public virtual int GetIncrementalDamageAddition(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
    {
        // note: can get owner from damageSource if necessary
        return 0;
    }

    /// <summary>
    ///  we add all multipliers together before actually using them.
    /// </summary>
    public virtual float GetIncrementalDamageMultiplier(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
    {
        // note: can get owner from damageSource if necessary
        return 0;
    }

    public virtual int GetIncrementalBlockAddition(int currentBaseBlock, AbstractCard blockSource, AbstractBattleUnit target)
    {
        return 0;
    }
}

public class BusterDamageModifier : DamageModifier
{
    public BusterDamageModifier()
    {
        this.Name = "Buster";
        this.Description = "Deal +50% damage to targets with Block.";
    }

    public override float GetIncrementalDamageMultiplier(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
    {
        if (target.CurrentBlock > 0)
        {
            return .5f;
        }
        return 0;
    }
}


public class PrecisionDamageModifier : DamageModifier
{

    public PrecisionDamageModifier()
    {
        this.Name = "Precision";
        this.Description = "Ignores Block.  If attacking a Marked target, gain +50% damage.";
        IgnoresDefense = true;
    }

    public override float GetIncrementalDamageMultiplier(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
    {
        if (target.HasStatusEffect<MarkedStatusEffect>())
        {
            return .5f;
        }
        return 0;
    }
}

public class SlayerDamageModifier : DamageModifier
{
    public SlayerDamageModifier()
    {
        this.Name = "Anti-Titan";
        this.Description = "Deals +50% damage to targets with >300 maximum hit points.";
    }
    public override float GetIncrementalDamageMultiplier(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
    {
        if (target.MaxHp > 300)
        {
            return .5f;
        }
        return 0;
    }
}
public class SweeperDamageModifier : DamageModifier
{
    public SweeperDamageModifier()
    {
        this.Name = "Sweeper";
        this.Description = "Deals 30% inflicted damage to up to 2 other random targets.  (Does not proc on-hits.)";
    }

    public override void OnStrike(AbstractCard damageSource, AbstractBattleUnit target, int preBlockDamage)
    {
        var otherPossibleTargets = GameState.Instance.EnemyUnitsInBattle
            .Where(item => item != target)
            .Where(item => item.IsTargetable())
            .Shuffle()
            .TakeUpTo(2)
            .ToList();
        ActionManager.Instance.DamageUnitNonAttack(target, damageSource.Owner, (int)(.3f * preBlockDamage));
    }
}

public class StrengthScalingDamageModifier : DamageModifier
{
    private int additionalScaling = 0;
    public StrengthScalingDamageModifier(int additionalStrengthScaling)
    {
        this.Name = "Additional Strength Scaling";
        this.Description = $"This attack gains {1 + additionalStrengthScaling}x damage from strength";
        this.additionalScaling = additionalStrengthScaling;
        this.TargetInvariant = true;
    }

    public override int GetIncrementalDamageAddition(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
    {
        return additionalScaling * damageSource?.Owner?.GetStatusEffect<StrengthStatusEffect>()?.Stacks ?? 0;
    }

}

public class LethalTriggerDamageModifier: DamageModifier
{
    private Action<AbstractBattleUnit> actToPerform;
    public LethalTriggerDamageModifier(string actionDescription, Action<AbstractBattleUnit> action)
    {
        this.actToPerform = action;
        this.Name = "Lethal";
        this.Description = actionDescription;
    }

    public override void Slay(AbstractCard damageSource, AbstractBattleUnit target)
    {
        actToPerform(target);
        BattleRules.ProcessProc(new LethalTriggerProc());
    }
}

public class LethalTriggerProc: AbstractProc
{
}

public static class BountyDamageModifier
{
    public static LethalTriggerDamageModifier GetBountyDamageModifier()
    {
        return new LethalTriggerDamageModifier("If this unit kills a:  Boss -> 20 credits, Elite -> 10 credits, other non-minion -> 5 credits.", (deadEnemy) =>
        {
            if (deadEnemy.IsBoss)
            {
                CardAbilityProcs.ChangeMoney(20);
            }
            if (deadEnemy.IsElite)
            {
                CardAbilityProcs.ChangeMoney(10);
            }
            else
            {
                // todo: Minion exclusion
                CardAbilityProcs.ChangeMoney(5);
            }
        });
    }
}

// If this targets the owner of the card, it applies +4 defense.
public class SelfishDefenseModifier: DamageModifier
{
    public override int GetIncrementalBlockAddition(int currentBaseBlock, AbstractCard blockSource, AbstractBattleUnit target)
    {
        if (blockSource.Owner == target)
        {
            return 4;
        }
        return 0;
    }
}

/// <summary>
/// If I have less Might [todo: Might is just the net damage increase] than the target, apply +50% defense.
/// </summary>
public class ProtectorDefenseModifier: DamageModifier
{
    //todo
}