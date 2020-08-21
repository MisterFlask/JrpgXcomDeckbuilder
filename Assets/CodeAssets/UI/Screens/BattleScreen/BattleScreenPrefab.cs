using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BattleScreenPrefab : MonoBehaviour
{
    public List<BattleUnitPrefab> PotentialBattleEntityEnemySpots;
    public List<BattleUnitPrefab> PotentialBattleEntityAllySpots;

    public void Setup(List<AbstractBattleUnit> StartingEnemies, List<AbstractBattleUnit> StartingAllies
        )
    {
        if (StartingAllies.Count > PotentialBattleEntityAllySpots.Count)
        {
            throw new System.Exception("Too many allies for availabel number of spots");
        }
        if (StartingEnemies.Count > PotentialBattleEntityEnemySpots.Count)
        {
            throw new System.Exception("Too many enemies for availabel number of spots");

        }
        PotentialBattleEntityEnemySpots = PotentialBattleEntityEnemySpots.Shuffle().ToList();
        PotentialBattleEntityAllySpots = PotentialBattleEntityAllySpots.Shuffle().ToList();
        for(int i = 0; i < StartingAllies.Count; i++)
        {
            PotentialBattleEntityAllySpots[i].Initialize(StartingAllies[i]);
        }
        for (int i = 0; i < StartingEnemies.Count; i++)
        {
            PotentialBattleEntityEnemySpots[i].Initialize(StartingEnemies[i]);
        }
    }
}
