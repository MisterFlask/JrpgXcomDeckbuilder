using UnityEngine;
using System.Collections;

public class EveryoneGetsSickPunishment : MissionFailurePunishment
{
    public int Stacks { get; set; }

    public override string Description()
    {
        return "All characters get 4 stacks of Sickness";
    }

    public override void OnFailure()
    {
        GameState.Instance.PersistentCharacterRoster.ForEach(character =>
        {
            character.ApplySoldierPerk(new SickenedSoldierPerk(), 4);
        });
    }

}

public class SickenedSoldierPerk: SoldierPerk
{
    public override void PerformAtBeginningOfNewDay(AbstractBattleUnit soldierAffected)
    {
        DecrementStacks(soldierAffected);
    }

    public override void PerformAtBeginningOfCombat(AbstractBattleUnit soldierAffected)
    {
        soldierAffected.AddStatusEffect(new PowerStatusEffect(), -1 * Stacks);
    }
}


