using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class KeroseneSoakedAxe : AbstractCard
{
    public KeroseneSoakedAxe()
    {
        SetCommonCardAttributes("Kerosene-Soaked Axe", Rarity.UNCOMMON, TargetType.ENEMY, CardType.AttackCard, 2);
    }
    public override string DescriptionInner()
    {
        return $"Deal 14 damage to an enemy.  Apply 10 Flammable.";
    }

    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage);
        action().ApplyStatusEffect(target, new FlammableStatusEffect(), 10);
    }
}
