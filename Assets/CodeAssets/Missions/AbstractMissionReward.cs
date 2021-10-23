using UnityEngine;
using System.Collections;
using Assets.CodeAssets.BattleEntities;

public abstract class AbstractMissionReward
{
    public abstract string Description();

    public ProtoGameSprite ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon();
    public abstract void OnReward();
}
public class RandomAugmentationMissionReward : AbstractMissionReward
{
    AbstractSoldierPerk randomizedAugmentation;
    public RandomAugmentationMissionReward()
    {
        randomizedAugmentation = PerkAndAugmentationRegistrar.GetRandomAugmentation(Rarity.ANY);
    }

    public override string Description()
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
    public override string Description()
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

    public override string Description()
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

    public override string Description()
    {
        return $"Travel to the next Circle.";
    }

    public override void OnReward()
    {
        GameState.Instance.NextRegionUnlocked = true;
    }
}