using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Assets.CodeAssets.UI.Subscreens;

public class GameScenes 
{
    public static void SwitchToBattleScene(AbstractMission mission, List<AbstractBattleUnit> CharactersSent)
    {
        if (CharactersSent.IsEmpty())
        {
            throw new System.Exception("No characters in roster!");
        }
        GameState.Instance.AllyUnitsInBattle = CharactersSent;
        GameState.Instance.EnemyUnitsInBattle = mission.EnemySquad.Members;
        GameState.Instance.CurrentMission = mission;
        SceneManager.LoadScene(sceneName: "BattleMapScene");

    }
    public static void RosterScreen()
    {
        SceneManager.LoadScene(sceneName: "CampaignMapScene");
    }

    public static void StsMapScene()
    {
        SceneManager.LoadScene(sceneName: "NewStsMapScene");
    }

    public static void SwitchToBattleResultSceneAndProcessCombatResults(CombatResult combatResult)
    {
        BattleRules.ProcessCombatResults(combatResult);
        SceneManager.LoadScene(sceneName: "BattleResultScene");
    }
}
