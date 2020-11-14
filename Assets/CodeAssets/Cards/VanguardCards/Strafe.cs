using UnityEngine;
using System.Collections;

public class Strafe : AbstractCard
{

    public Strafe()
    {
        BaseDamage = 2;
    }

    public override string Description()
    {
        return $"Deal {DisplayedDamage()} damage to ALL enemies.";
    }
    protected override void OnPlay(AbstractBattleUnit target)
    {
        CardActions.ActOnAllEnemies((enemy) =>
        {
            ActionManager.Instance.AttackUnitForDamage(enemy, Owner, BaseDamage);
        });

    }
}
