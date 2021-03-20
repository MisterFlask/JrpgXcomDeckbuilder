using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmoredEnemyUnit : AbstractEnemyUnit
{
        public ArmoredEnemyUnit()
        {
            this.CharacterFullName = "ArmoredEnemy";
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/Machines/RoboVAK", color: Color.blue);
            this.MaxHp = 30;
            this.ApplyStatusEffect(new ArmoredStatusEffect(), stacks: 4);
        }

    public override List<AbstractIntent> GetNextIntents()
    {
        return Intents.FixedRotation(
            Intents.BuffSelfOrHeal(this, new StrengthStatusEffect(), 5),
            Intents.AttackRandomPc(this, 1, 1)
        );

    }



}
