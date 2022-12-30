using Assets.CodeAssets.UI_V2;
using Assets.CodeAssets.UI_V2.RunMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    public GameObject nodeUIPrefab;
    public List<RoomNodeUI> Rooms;
    public LineRenderer LineRendererTemplatePrefab;
    
    public static MapUI INSTANCE => FindObjectOfType<MapUI>();

    /// <summary>
    ///  populated later
    /// </summary>
    public RunMapModel ModelObject;

    public List<RoomModel> GetRoomModels()
    {
        return Rooms.Select(room => room.ModelObject).ToList();
    }
    public RoomNodeUI GetRoomAt(int floor, int wing)
    {
        return Rooms.Single(item => item.Floor == floor && item.Wing == wing);
    }

    public List<RoomNodeUI> GetRoomsOnFloor(int floor)
    {
        return Rooms.Where(item => item.Floor == floor).ToList();
    }

    public bool IsRoomAccessible(int floor, int wing)
    {
        return ModelObject.isRoomCurrentlyAccessible(floor, wing);
    }

    public List<RoomNodeUI> RoomsReachableFromThisRoom(int level, int wing)
    {
        var modelObjectsOfPreviousFloor = ModelObject.Rooms.Where(target => ModelObject.IsRoomAccessibleFrom(level, wing, target.Floor, target.Wing));
        return GetCorrespondingRoomUiObjects(modelObjectsOfPreviousFloor);
    }
    
    public List<RoomNodeUI> GetCorrespondingRoomUiObjects(IEnumerable<RoomModel> models)
    {
        return Rooms.Where(item => models.Any(model => model.Floor == item.Floor && item.Wing == model.Wing)).ToList();
    }
    
    public RunMapModel RegenerateInitialModel()
    {
        Debug.Log("Regenerating initial run map model");
        this.Rooms = GetRoomsFromScene();
        
        foreach(var room in Rooms)
        {
            room.ModelObject = room.GenerateModelObject();
        }
        
        var mapModel = new RunMapModel
        {
            Rooms = GetRoomModels(),
            CurrentFloor = -1,
            CurrentWing = -1,
        };

        this.ModelObject = mapModel;

        foreach (var room in Rooms)
        {
            room.Initialize(room.ModelObject, mapModel, this);
        }

        mapModel.RandomizeConnectionsBasedOnGameRules();
        
        foreach (var room in Rooms)
        {
            room.RegenerateLinesBetweenRooms();
        }
        return mapModel;
    }

    public List<RoomNodeUI> GetRoomsFromScene()
    {
        List<RoomNodeUI> childComponents = new List<RoomNodeUI>();
        foreach (Transform child in transform)
        {
            RoomNodeUI component = child.GetComponent<RoomNodeUI>();
            if (component != null)
            {
                childComponents.Add(component);
            }
        }

        Debug.Log("Found " + childComponents.Count + " rooms in scene.");
        return childComponents;
    }
}