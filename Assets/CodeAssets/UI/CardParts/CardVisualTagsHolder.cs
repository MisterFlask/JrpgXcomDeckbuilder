using HyperCard;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.UI.CardParts
{
    public class CardVisualTagsHolder : MonoBehaviour
    {
        public Transform parent;
        public CardVisualTagPrefab TemplatePrefab;

        public List<CardVisualTag> CardTags { get; set; } = new List<CardVisualTag>();
        public Card Card;
         
        // Attempts to ensure that only exactly those card tags that are appropriate are displayed
        void Update()
        {
            if (parent == null || Card == null || Card.LogicalCard == null)
            {
                this.PurgeChildren();
                return;
            }

            this.PurgeChildrenIf<CardVisualTagPrefab>(item => item.CardTag == null);

            var children = GetComponentsInChildren<CardVisualTagPrefab>().ToList();
            var displayedAttributes = children.Select(item => item.CardTag).WhereNotNull();
            var currentVisualTags = Card.LogicalCard.CardVisualTags;

            foreach (var abstractVisualTag in currentVisualTags)
            {
                if (!displayedAttributes.Any(item => item.ProtoSprite.SpritePath == abstractVisualTag.ProtoSprite.SpritePath))
                {
                    var newPrefab = TemplatePrefab.Spawn(this.transform);
                    newPrefab.CardTag = abstractVisualTag;
                    abstractVisualTag.CorrespondingPrefab = newPrefab;
                }
                /* What is this for?
                if (abstractVisualTag.CorrespondingPrefab != null)
                {
                    abstractVisualTag.CorrespondingPrefab.gameObject.Despawn();
                    abstractVisualTag.CorrespondingPrefab = null;
                }
                */
                
            }
            var childrenToPurge = new List<CardVisualTag>();
            foreach (var attr in displayedAttributes)
            {
                if (!currentVisualTags.Contains(attr))
                {
                    childrenToPurge.Add(attr);
                }
            }
            this.PurgeChildrenIf<CardVisualTagPrefab>(item => childrenToPurge.Contains(item.CardTag));

        }
    }
}