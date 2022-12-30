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
        public SpriteRenderer SpriteRenderer;
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

        public Sprite VisitedSprite;
        public Sprite UnvisitedSprite;
        public Sprite CurrentSprite;
        public MapUI MapUi;

        public TextMeshPro NameText;
        public TextMeshPro DebugText;

        public List<LineRenderer> Lines { get; set; } = new List<LineRenderer>();
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

            modelObject.Floor = this.Floor;
            modelObject.Wing = this.Wing;
            
            Debug.Log($"Initializing room node ui for {modelObject.Floor} {modelObject.Wing}");

            //first kill all the line renderer objects

            this.SpriteRenderer = GetComponent<SpriteRenderer>();

            //this.VisitedSprite = Resources.Load(modelObject.VisitedSpriteName) as Sprite;
            //this.UnvisitedSprite = Resources.Load(modelObject.UnvisitedSpriteName) as Sprite;
            //this.CurrentSprite = Resources.Load(modelObject.PlayerLocationSpriteName) as Sprite;
        }

        public void RegenerateLinesBetweenRooms()
        {
            foreach (var line in Lines)
            {
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

        private void Update()
        {
            if (ModelObject == null)
            {
                Debug.LogError("No model object!");
                return;
            }
            //SpriteRenderer.sprite = ModelObject.Visited ? VisitedSprite : UnvisitedSprite;
            //SpriteRenderer.enabled = ModelObject.Current ? CurrentSprite : (ModelObject.Visited ? VisitedSprite : null);

            if (ModelObject.Visited)
            {
                this.SpriteRenderer.color = Color.cyan;
            }
            
            if (ModelObject.Current)
            {
                this.SpriteRenderer.color = Color.red;
            }

            this.DebugText.text = this.ModelObject.Floor + "," + this.ModelObject.Wing;
            this.NameText.text = $"node: [visited={ModelObject.Visited}, current={ModelObject.Current}, reachable={this.ParentMapUi.isRoomCurrentlyAccessible(this.Floor, this.Wing)}]";
        }
        
        void OnMouseDown()
        {
            Debug.Log("Mouse down over the room node: " + JsonConvert.ToString(this.ModelObject));
            if (this.ParentMapUi.isRoomCurrentlyAccessible(this.Floor, this.Wing))
            {
                ParentMapUi.TravelToRoom(this.Floor, this.Wing);
            }
        }
    }
}
