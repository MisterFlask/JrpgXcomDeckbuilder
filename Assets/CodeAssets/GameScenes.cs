using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Assets.CodeAssets.UI.Subscreens;

public class GameScenes 
{
    public static void SwitchToBattleScene(AbstractMission mission, List<AbstractBattleUnit> CharactersSent)
    {
        GameState.Instance.AllyUnitsInBattle = CharactersSent;
        GameState.Instance.EnemyUnitsInBattle = mission.EnemySquad.Members;
        GameState.Instance.CurrentMission = mission;
        SceneManager.LoadScene(sceneName: "BattleMapScene");

    }
    public static void MissionPrepScene()
    {
        SceneManager.LoadScene(sceneName: "CampaignMapScene");
    }

    public static void StsMapScene()
    {
        SceneManager.LoadScene(sceneName: "StsMapScene");
    }

    public static void SwitchToBattleResultSceneAndProcessCombatResults(CombatResult combatResult)
    {
        BattleRules.ProcessCombatResults(combatResult);
        SceneManager.LoadScene(sceneName: "BattleResultScene");
    }
}
