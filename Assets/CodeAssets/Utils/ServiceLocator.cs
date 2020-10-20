
using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using PathologicalGames;
using HyperCard;

public static class ServiceLocator
{
    private static Dictionary<string, GameObject> objectCache = new Dictionary<string, GameObject>(); //todo

    private static GameLogic gameLogic;

    public static UtilityObjectHolder UtilityObjectHolder;

    private static TemplateHolder GetTemplateHolder()
    {
        return UtilityObjectHolder.TemplateHolder;
    }

    public static TemplateHolder GameObjectTemplates()
    {
        return GetTemplateHolder();
    }

    private static Dictionary<Type, MonoBehaviour> typeToObjectDict = new Dictionary<Type, MonoBehaviour>();//todo

    public static ExplainerPanel TooltipPanel()
    {
        return UtilityObjectHolder.ExplainerPanel;
    }

    public static CardInstantiator GetCardInstantiator()
    {
        return UtilityObjectHolder.CardInstantiator;
    }

    public static GameLogic GameLogic()
    {
        if (gameLogic == null)
        {
            gameLogic = new GameLogic();
        }
        return gameLogic;
    }

    #region UI
    public static GameObject GetUiCanvas()
    {
        return UtilityObjectHolder.UiCanvas;
    }

    public static CameraController GetCamera()
    {
        return GameObject.Find("MAIN_CAMERA").GetComponent<CameraController>();
    }

    public static SpawnPool GetSpawnPool()
    {
        if (UtilityObjectHolder == null)
        {
            return GameObject.FindObjectOfType<SpawnPool>();
        }

        return UtilityObjectHolder.SpawnPool;
    }
    public static ActionManager GetActionManager()
    {
        return UtilityObjectHolder.ActionManager;
    }

    public static CardAnimationManager GetCardAnimationManager()
    {
        return UtilityObjectHolder.CardAnimationManager;
    }

    public static GameState GetGameStateTracker()
    {
        return GameState.Instance;
    }

    public static UiStateManager GetUiStateManager()
    {
        return GameObject.FindObjectOfType<UiStateManager>();
    }

    public static Transform GetUnitFolder()
    {
        return GetTemplateHolder().UnitHolder.transform;
    }
    #endregion

    public static MenuHolder MenuHolder = new MenuHolder();

}

public class MenuHolder
{
    public BattleWonMenu BattleWonMenu { get; set; }
}