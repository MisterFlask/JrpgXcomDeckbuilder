using UnityEngine;
using System.Collections;
using Assets.CodeAssets.Cards;

public class Strafe : AbstractCard
{

    public Strafe()
    {
        BaseDamage = 6;
        CardType = CardType.AttackCard;
        TargetType = TargetType.ENEMY;
        this.Rarity = Rarity.COMMON;
    }

    public override string DescriptionInner()
    {
        return $"Deal {DisplayedDamage()} damage to ALL enemies.";
    }
    public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
    {
        CardActions.ActOnAllEnemies((enemy) =>
        {
            ActionManager.Instance.AttackUnitForDamage(enemy, Owner, BaseDamage);
        });

    }
}
