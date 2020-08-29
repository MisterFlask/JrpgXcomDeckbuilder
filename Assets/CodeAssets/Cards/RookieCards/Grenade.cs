﻿using UnityEngine;
using System.Collections;

public class Grenade : AbstractCard
{
    public Grenade()
    {
        this.Damage = 5;
        TargetType = TargetType.ENEMY;
        Name = "Grenade";
    }

    public override string Description()
    {
        return "Deals 5 damage to the target, then 5 damage to all enemies.  Expend.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().AttackUnitForDamage(target, 5);

        foreach(var otherTarget in enemies())
        {
            action().AttackUnitForDamage(otherTarget, 5);
        }
        action().ExpendCard(this);
    }
}
