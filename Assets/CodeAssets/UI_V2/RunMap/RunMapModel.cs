using Assets.CodeAssets.UI_V2.RunMap;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.CodeAssets.UI_V2
{
    public class RunMapModel
    {
        public List<RoomModel> Rooms;
        public int CurrentFloor = -1;
        public int CurrentWing = -1;

        //assumes we already have the Rooms list populated and merely need to connect them and assign any additional randomizers.
        // For each room, we do the following: (1) check which wings exist on the next floor, and then (2) we assign two random wings to be reachable from this room.
        public void RandomizeConnectionsBasedOnGameRules()
        {
            foreach (var currentRoom in Rooms)
            {
                var nextFloorRooms = Rooms.Where(item => item.Floor == currentRoom.Floor + 1).ToList();
                var wingOptions = new List<int>() { currentRoom.Wing, currentRoom.Wing - 1, currentRoom.Wing + 1 };
                var randomSelection = wingOptions.PickRandom(2);
                foreach (var wing in randomSelection)
                {
                    var targetRoom = (GetRoom(currentRoom.Floor + 1, wing));
                    if (targetRoom != null)
                    {
                        currentRoom.ReachableWingsFromHere.Add(wing);
                    }
                }
            }
        }
        public bool isRoomCurrentlyAccessible(int targetFloor, int targetWing)
        {
            var currentRoom = GetRoom(CurrentFloor, CurrentWing);
            return IsRoomAccessibleFrom(currentRoom, targetFloor, targetWing);
        }
        public bool IsRoomAccessibleFrom(int currentFloor, int currentWing, int targetFloor, int targetWing)
        {
            return IsRoomAccessibleFrom(GetRoom(currentFloor, currentWing), targetFloor, targetWing);
        }
   
        public bool IsRoomAccessibleFrom(RoomModel currentRoom, int targetFloor, int targetWing)
        {
            var targetRoom = GetRoom(targetFloor, targetWing);
            
            if (targetRoom == null)
            {
                return false;
            }
            
            if (targetRoom.Floor != CurrentFloor + 1)
            {
                return false;
            }

            // this means we're in our starting location
            if (currentRoom == null)
            {
                return true;
            }
            
            if (currentRoom.ReachableWingsFromHere.Contains(targetWing))
            {
                return true;
            }

            return false;
        }

        public RoomModel GetRoom(int floor, int wing)
        {
            return Rooms.SingleOrDefault(item => item.Wing == wing && item.Floor == floor);
        }
         public string Serialize()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(this, settings);
        }

        public static RunMapModel Deserialize(string serialized)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Formatting = Formatting.Indented,
            };

            RunMapModel deserialized = JsonConvert.DeserializeObject<RunMapModel>(serialized, settings);

            return deserialized;
        }

        internal void TravelToRoom(int floor, int wing)
        {
            GetRoom(CurrentFloor, CurrentWing).Visited = true;
            GetRoom(CurrentFloor, CurrentWing).Current = false;

            CurrentFloor = floor;
            CurrentWing = wing;

            GetRoom(CurrentFloor, CurrentWing).Current = true;
        }
    }

public class RoomModel
    {
        [JsonIgnore]
        public RunMapModel MapModel { get; set; }
        public List<int> ReachableWingsFromHere = new List<int>();
        
        public string VisitedSpriteName { get;  set; } = "MapNodeVisited";
        public string UnvisitedSpriteName { get;  set; } = "MapNodeUnvisited";
        public string PlayerLocationSpriteName { get;  set; } = "MapNodeCurrent";

        public int Floor;
        public int Wing;
        public bool Visited;
        public bool Current;

        // presumed to be used by Newtonsoft ser/de
        public RoomModel()
        {
        }
        
        public RoomModel(int floor, int wing)
        {
            this.Floor = floor ;
            this.Wing = wing;
        }
    }
}
