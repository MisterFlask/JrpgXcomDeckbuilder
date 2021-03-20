using Assets.CodeAssets.Cards.ArchonCards.Effects;
using System;
using System.Collections;
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

    public virtual void OnStrike(AbstractCard damageSource, AbstractBattleUnit target)
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

public class AntiTitanDamageModifier : DamageModifier
{
    public AntiTitanDamageModifier()
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
public class AntiPersonnelDamageModifier : DamageModifier
{
    public AntiPersonnelDamageModifier()
    {
        this.Name = "Anti-Personnel";
        this.Description = "Applies 5 Terror on hit.  Deal +50% damage against enemies with the Horde characteristic.";
    }
    public override float GetIncrementalDamageMultiplier(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
    {
        //todo
        return 0;
    }

    public override void OnStrike(AbstractCard damageSource, AbstractBattleUnit target)
    {
        target.ApplyStatusEffect(new TerrorStatusEffect(), 5);
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

public class SlayRule: DamageModifier
{
    private Action<AbstractBattleUnit> actToPerform;
    public SlayRule(string actionDescription, Action<AbstractBattleUnit> action)
    {
        this.actToPerform = action;
        this.Name = "Slay";
        this.Description = actionDescription;
        
    }

    public override void Slay(AbstractCard damageSource, AbstractBattleUnit target)
    {
        actToPerform(target);
    }
}