﻿using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class KeroseneSoakedAxe : AbstractCard
{
    public KeroseneSoakedAxe()
    {
        SetCommonCardAttributes("Kerosene-Soaked Axe", Rarity.UNCOMMON, TargetType.ENEMY, CardType.AttackCard, 2);
        this.DamageModifiers.Add(new SlayerDamageModifier());
    }
    public override string DescriptionInner()
    {
        return $"Deal 14 damage to an enemy.  Apply 10 Fumes.  Slayer.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage, this);
        action().ApplyStatusEffect(target, new FumesStatusEffect(), 10);
    }
}
