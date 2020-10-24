using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameScenes 
{
    public static void SwitchToBattleScene(Mission mission, List<AbstractBattleUnit> CharactersSent)
    {
        GameState.Instance.AllyUnitsInBattle = CharactersSent;
        GameState.Instance.EnemyUnitsInBattle = mission.StartingEnemies();
        SceneManager.LoadScene(sceneName: "BattleMapScene");

    }
    public static void SwitchToCampaignScene()
    {
        SceneManager.LoadScene(sceneName: "CampaignMapScene");
    }
    public static void SwitchToBattleResultScene()
    {
        SceneManager.LoadScene(sceneName: "BattleResultScene");
    }
}
