﻿using UnityEngine;
using System.Collections;
using Assets.CodeAssets.BattleEntities;

public abstract class AbstractMissionReward
{
    public abstract string GenericDescription();
    public virtual string GetSpecificDescription()
    {
        return GenericDescription();
    }

    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon();
    public abstract void OnReward();
}
public class RandomAugmentationMissionReward : AbstractMissionReward
{
    AbstractSoldierPerk randomizedAugmentation;
    public RandomAugmentationMissionReward()
    {
        randomizedAugmentation = PerkAndAugmentationRegistrar.GetRandomAugmentation(Rarity.ANY);
    }

    public override string GenericDescription()
    {
        return $"Gain a random augmentation to inventory.";
    }

    public override void OnReward()
    {
        // Show augmentation acquired screen
        GameState.Instance.AugmentationInventory.Add(randomizedAugmentation);
        
    }
}

public class SoldierMissionReward: AbstractMissionReward
{
    int level;
    AbstractSoldierClass clazz;

    public SoldierMissionReward(AbstractSoldierClass clazz, int level = 1)
    {
        this.level = level;
        this.clazz = clazz;
    }
    public override string GenericDescription()
    {
        return $"Gain a level {level} {clazz.Name()} to your roster";
    }

    public override void OnReward()
    {
        GameState.Instance.PersistentCharacterRoster.Add(
            Soldier.GenerateSoldierOfClass(clazz, level));
    }
}

public class MoneyMissionReward : AbstractMissionReward
{
    int quantity;
    public MoneyMissionReward(int quantity)
    {
        this.quantity = quantity;
    }

    public override string GenericDescription()
    {
        return $"Gain ${quantity}";
    }

    public override void OnReward()
    {
        GameState.Instance.Credits+=quantity;
    }
}

public class GateBypassMissionReward : AbstractMissionReward
{
    public GateBypassMissionReward()
    {
    }

    public override string GenericDescription()
    {
        return $"Travel to the next Circle.";
    }

    public override void OnReward()
    {
        GameState.Instance.NextRegionUnlocked = true;
        GameState.Instance.GateMissionUnlocked = false;
    }
}
public class GateKeyMissionReward : AbstractMissionReward
{
    public GateKeyMissionReward()
    {
    }

    public override string GenericDescription()
    {
        return $"Enables taking on a Gate mission.";
    }

    public override void OnReward()
    {
        GameState.Instance.GateMissionUnlocked = true;
    }
}