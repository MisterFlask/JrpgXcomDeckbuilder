using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmoredEnemyUnit : AbstractEnemyUnit
{
        public ArmoredEnemyUnit()
        {
            this.CharacterName = "ArmoredEnemy";
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/RoboVAK", color: Color.blue);
            this.MaxHp = 30;
            this.AddStatusEffect(new ArmoredStatusEffect(), stacks: 4);
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
