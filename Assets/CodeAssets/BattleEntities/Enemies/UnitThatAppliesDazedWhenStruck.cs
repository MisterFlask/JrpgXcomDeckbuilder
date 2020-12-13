using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitThatAppliesDazedWhenStruck : AbstractEnemyUnit
{
    public UnitThatAppliesDazedWhenStruck()
    {
        this.CharacterName = "Drone";
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/Charger1", color: Color.white);
        this.MaxHp = 30;
        this.AddStatusEffect(new ApplyDazedOnHit(), stacks: 4);
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        return new List<AbstractIntent>
        {
            new BuffSelfIntent(this, new PowerStatusEffect()),
            new BuffSelfIntent(this, new PowerStatusEffect()),
            SingleUnitAttackIntent.AttackRandomEnemy(this, 10, 1),
            SingleUnitAttackIntent.AttackRandomEnemy(this, 15, 1),
            SingleUnitAttackIntent.AttackRandomEnemy(this, 4, 2)
        }
        .PickRandom()
        .ToSingletonList();
    }
}

public class ApplyDazedOnHit : AbstractStatusEffect
{
    public ApplyDazedOnHit()
    {
        Name = "Static";
    }

    public override string Description => "Adds a Distracted to your discard pile when damaged.";

    public override void OnStruck(AbstractBattleUnit unitStriking, int totalDamageTaken)
    {
        action().AddCardToDrawPile(new Distracted());
    }
}