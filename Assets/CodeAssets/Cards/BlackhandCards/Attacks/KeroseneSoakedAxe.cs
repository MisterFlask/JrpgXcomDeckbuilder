using UnityEngine;
using System.Collections;

public class KeroseneSoakedAxe : AbstractCard
{
    public KeroseneSoakedAxe()
    {
        SetCommonCardAttributes("Kerosene-Soaked Axe", Rarity.UNCOMMON, TargetType.ENEMY, CardType.AttackCard, 2);
    }
    public override string Description()
    {
        return $"Deal 14 damage to an enemy.  Apply 10 Flammable.";
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().AttackUnitForDamage(target, this.Owner, BaseDamage);
        action().ApplyStatusEffect(target, new FlammableStatusEffect(), 10);
    }
}
