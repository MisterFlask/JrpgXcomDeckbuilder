using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BattleScreenPrefab : MonoBehaviour
{
    public List<BattleEntity> StartingEnemies { get; set; }
    public List<BattleEntity> StartingAllies { get; set; }

    public List<BattleEntityPrefab> PotentialBattleEntityEnemySpots;
    public List<BattleEntityPrefab> PotentialBattleEntityAllySpots;

    public void Setup()
    {
        PotentialBattleEntityEnemySpots = PotentialBattleEntityEnemySpots.Shuffle().ToList();
        PotentialBattleEntityAllySpots = PotentialBattleEntityAllySpots.Shuffle().ToList();
        for(int i = 0; i < StartingAllies.Count; i++)
        {
            // todo
        }
    }
}
