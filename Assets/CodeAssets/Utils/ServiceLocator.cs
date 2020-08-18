
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

    private static TemplateHolder _templateHolder;
    private static TemplateHolder GetTemplateHolder()
    {
        if (_templateHolder == null)
        {
            _templateHolder = GameObject.FindObjectOfType<TemplateHolder>();
        }
        return _templateHolder;
    }

    public static TemplateHolder GameObjectTemplates()
    {
        return GetTemplateHolder();
    }


    private static GameObject GetObjectFromCache(string item)
    {
        if (objectCache.ContainsKey(item))
        {
            return objectCache[item];
        }
        else
        {
            objectCache[item] = GameObject.Find(item);
            return objectCache[item];
        }
    }

    private static Dictionary<Type, MonoBehaviour> typeToObjectDict = new Dictionary<Type, MonoBehaviour>();//todo

    public static T GetSingletonObjectOfType<T>() where T : MonoBehaviour
    {
        var type = typeof(T);
        if (typeToObjectDict.ContainsKey(type))
        {
            return typeToObjectDict[type].GetComponent<T>();
        }

        var objs = GameObject.FindObjectsOfType<T>();
        if (objs.IsEmpty())
        {
            throw new Exception($"Cannot find object of type {typeof(T).Name}");
        }
        if (objs.Count() > 1)
        {
            throw new Exception($"Found MORE THAN ONE object of type {typeof(T).Name}");
        }
        typeToObjectDict[type] = objs.Single();
        return objs.Single();
    }
    public static ExplainerPanel private_ExplainerPanel;
    public static ExplainerPanel TooltipPanel()
    {
        return private_ExplainerPanel;
    }
    public static HexAttributePrefab GetHexAttributePrefabTemplate()
    {
        return GameObject.Find("ExampleTileAttribute").GetComponent<HexAttributePrefab>(); //TODO: Rationalize naming
    }

    static CardInstantiator cardInstantiator = null;
    public static CardInstantiator GetCardInstantiator()
    {
        if (cardInstantiator == null)
        {
            cardInstantiator = GameObject.FindObjectOfType<CardInstantiator>();
        }
        return cardInstantiator;
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
    public static GameObject GetMissionDisplayPanel()
    {
        return GameObject.Find("MISSION_DISPLAY_PANEL");
    }
    public static GameObject GetUiCanvas()
    {
        return GameObject.Find("UI_CANVAS");
    }

    public static CameraController GetCamera()
    {
        return GameObject.Find("MAIN_CAMERA").GetComponent<CameraController>();
    }

    public static GameObject GetMissionDisplayPanelTitle()
    {
        return GameObject.Find("MISSION_DISPLAY_PANEL");
    }
    public static GameObject GetMissionDisplayPanelContentPane()
    {
        return GameObject.Find("MISSION_DISPLAY_CONTENT_PANE");
    }
    private static SpawnPool spawnPool = null;
    public static SpawnPool GetSpawnPool()
    {
        if (spawnPool == null)
        {
            spawnPool = GameObject.Find("SpawnPool").GetComponent<SpawnPool>();
        }
        return spawnPool;
    }
    public static ActionManager GetActionManager()
    {
        return GetSingletonObjectOfType<ActionManager>();
    }

    public static TileMap GetTileMap()
    {
        return GetSingletonObjectOfType<TileMap>();
    }


    private static CardAnimationManager handManager = null;
    public static CardAnimationManager GetCardAnimationManager()
    {
        if (handManager == null)
        {
            handManager = GetSingletonObjectOfType<CardAnimationManager>();
        }
        return handManager;
    }

    public static GameObject GetAllMissionsContentPanel()
    {
        return GameObject.Find("ALL_MISSIONS_CONTENT_PANE");
    }

    public static GameState GetGameStateTracker()
    {
        return GameObject.Find("GAME_STATE_TRACKER").GetComponent<GameState>();
    }

    internal static MissionSelector GetMissionSelectorTemplate()
    {
        return GameObject.Find("MISSION_SELECTOR_TEMPLATE").GetComponent<MissionSelector>();
    }

    public static GameObject GetMissionObjectiveShortDescriptionTemplate()
    {
        return GameObject.Find("OBJECTIVE_TEMPLATE");
    }

    public static UiStateManager GetUiStateManager()
    {
        return GameObject.FindObjectOfType<UiStateManager>();
    }

    public static SpriteRenderer GetBlurryWhiteOutlineForHexes()
    {
        return GameObject.Find("BLURRY_WHITE_OUTLINE").GetComponent<SpriteRenderer>();
    }

    public static GameObject GetHandObject()
    {
        return GameObject.Find("Hand");
    }

    public static RivalUnitPrefab GetRivalUnitPrefab()
    {
        return GetTemplateHolder().RivalUnitPrefab;
    }
    public static Transform GetUnitFolder()
    {
        return GetTemplateHolder().UnitHolder.transform;
    }
    #endregion
}
