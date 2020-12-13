using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrainCrab : AbstractEnemyUnit
{

    public BrainCrab()
    {
        this.CharacterName = "Brain Crab";
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/RoboVAK", color: Color.white);
        this.MaxHp = 14;
        this.AddStatusEffect(new AddsParasiteOnDealingDamage(), stacks: 4);
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        return new List<AbstractIntent>
        {
            new BuffSelfIntent(this, new PowerStatusEffect(), 5),
            SingleUnitAttackIntent.AttackRandomEnemy(this, 1, 1)
        }
        .PickRandom()
        .ToSingletonList();
    }

}
public class AddsParasiteOnDealingDamage : AbstractStatusEffect
{
    //todo

    public AddsParasiteOnDealingDamage()
    {
        Name = "Parasitoid";
        ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/falling-bang", Color.yellow);
    }


    public override string Description => "Whenever this deals unblocked damage, adds a Parasite to your discard pile";
}
