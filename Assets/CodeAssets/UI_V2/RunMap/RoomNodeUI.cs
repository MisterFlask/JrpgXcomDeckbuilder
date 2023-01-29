using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.CodeAssets.UI_V2.RunMap
{
    public class RoomNodeUI : MonoBehaviour
    {
        /// <summary>
        /// required to set in editor
        /// </summary>
        public FramedImage SpriteRenderer;
        /// <summary>
        /// We can access 
        /// </summary>
        public int Floor;
        public int Wing;

        /// <summary>
        ///  populated later
        /// </summary>
        public RoomModel ModelObject;
        public RunMapModel ParentMapUi;
        
        public MapUI MapUi;

        public TextMeshPro NameText;
        public TextMeshPro DebugText;

        public bool isInitialized;
        public MapNodeBehavior Behavior { get; set; }
        public List<LineRenderer> Lines { get; set; } = new List<LineRenderer>();

        public float initialSpriteWidth;
        public float initialSpriteHeight;
        
        public IEnumerable<RoomNodeUI> GetReachableNodesFromThisFloor()
        {
            return MapUi.RoomsReachableFromThisRoom(Floor, Wing);
        }

        public RoomModel GenerateModelObject()
        {
            return new RoomModel
            {
                Wing = Wing,
                Floor = Floor,
                Visited = false,
                VisitedSpriteName = "visitedsprite",
                UnvisitedSpriteName = "unvisitedsprite",
                PlayerLocationSpriteName = "playerlocationsprite",
            };
        }

        private List<RoomNodeUI> _reachableNodes;


        public void Initialize(RoomModel modelObject, RunMapModel parentMapUi, MapUI mapUi)
        {
            Require.NotNull(modelObject);
            Require.NotNull(parentMapUi);
            Require.NotNull(mapUi);

            this.MapUi = mapUi;
            this.ModelObject = modelObject;
            this.ParentMapUi = parentMapUi;
            isInitialized = true;
            modelObject.Floor = this.Floor;
            modelObject.Wing = this.Wing;
            this.Behavior = MapNodeBehavior.GetRandomBehaviorForFloor(modelObject.Floor);
            SpriteRenderer.Subject.SetIcon(Behavior.PrimaryProtoSprite.ToSprite());
            
            Debug.Log($"Initializing room node ui for {modelObject.Floor} {modelObject.Wing}");

            //first kill all the line renderer objects

            //this.VisitedSprite = Resources.Load(modelObject.VisitedSpriteName) as Sprite;
            //this.UnvisitedSprite = Resources.Load(modelObject.UnvisitedSpriteName) as Sprite;
            //this.CurrentSprite = Resources.Load(modelObject.PlayerLocationSpriteName) as Sprite;
            
            
        }

        public void RegenerateLinesBetweenRooms()
        {
            foreach (var line in Lines)
            {
                line.enabled = false;
                Destroy(line);
            }

            this.Lines = new List<LineRenderer>();
            
            foreach (RoomNodeUI reachableNode in GetReachableNodesFromThisFloor())
            {
                Debug.Log("Regenerating reachable node lines list");
                LineRenderer lineRenderer = Instantiate(MapUi.LineRendererTemplatePrefab);
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, reachableNode.transform.position);
                lineRenderer.startWidth = 0.2f;
                lineRenderer.endWidth = 0.2f;
                Lines.Add(lineRenderer);
            }
        }

        public void Start()
        {
        }

        private void Update()
        {
            if (!isInitialized)
            {
                return;
            }
            
            if (ModelObject == null)
            {
                Debug.LogError("No model object!  " + this.Floor + " " + this.Wing);
                return;
            }
            //SpriteRenderer.sprite = ModelObject.Visited ? VisitedSprite : UnvisitedSprite;
            //SpriteRenderer.enabled = ModelObject.Current ? CurrentSprite : (ModelObject.Visited ? VisitedSprite : null);

            if (ModelObject.Visited)
            {
                SpriteRenderer.Subject.Renderer.color = Color.cyan;
            }
            
            if (ModelObject.Current)
            {
                SpriteRenderer.Subject.Renderer.color = Color.red;
            }

             // todo: secondary protosprite
            this.DebugText.text = this.ModelObject.Floor + "," + this.ModelObject.Wing;
            this.NameText.text = $"node: [visited={ModelObject.Visited}, current={ModelObject.Current}, reachable={this.ParentMapUi.isRoomCurrentlyAccessible(this.Floor, this.Wing)}]";
        }
        void SetSpriteAndResize(SpriteRenderer renderer, Sprite sprite)
        {
            ProtoGameSprite.SetSpriteRendererToSpriteWhileMaintainingSize(initialSpriteWidth, initialSpriteHeight, sprite, renderer);
         }

        public void MouseDownClickHandler()
        {
            Debug.Log("Mouse down over the room node");
            if (this.ParentMapUi.isRoomCurrentlyAccessible(this.Floor, this.Wing))
            {
                Debug.Log("Moving to wing: " + this.Floor + " , " + this.Wing);
                ParentMapUi.TravelToRoom(this.Floor, this.Wing);
                Behavior.OnEnterNode();
            }
            else
            {
                Debug.Log("Wing inaccessible: " + this.Floor + " , " + this.Wing);
            }
        }

    }
}
