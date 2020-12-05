using UnityEngine;
using System.Collections;

public class Firewall : AbstractCard
{
    int TemporaryThornsGranted = 7;

    public Firewall()
    {

        BaseDefenseValue = 10;
        SetCommonCardAttributes("Firewall", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 1);
    }

    public override string Description()
    {
        return $"Apply {DisplayedDefense()} to ally.  Grant that ally {TemporaryThornsGranted} Temporary Thorns."
    }

    protected override void OnPlay(AbstractBattleUnit target)
    {
        action().ApplyDefense(target, Owner, BaseDefenseValue);
        action().ApplyStatusEffect(target, new TemporaryThorns(), TemporaryThornsGranted);
    }
}
