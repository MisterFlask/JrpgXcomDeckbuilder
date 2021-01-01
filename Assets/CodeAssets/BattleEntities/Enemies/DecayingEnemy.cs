using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecayingEnemy : AbstractEnemyUnit
{


    public DecayingEnemy()
    {
        this.CharacterName = "DecayingEnemy";
        this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/RoboVAK", color: Color.white);
        this.MaxHp = 300;
        this.AddStatusEffect(new MalfunctioningStatusEffect(), stacks: 1);
    }

    public override List<AbstractIntent> GetNextIntents()
    {
        return new List<AbstractIntent>
        {
            new BuffSelfIntent(this, new PowerStatusEffect(), 5),
            SingleUnitAttackIntent.AttackRandomPc(this, 1, 1)
        }
        .PickRandom()
        .ToSingletonList();
    }
}

public class MalfunctioningStatusEffect : AbstractStatusEffect
{
    //wheneever this gets attacked, applies 1 burning.
    public override string Description => "Whenever this is attacked, applies 1 Burning.";
}
