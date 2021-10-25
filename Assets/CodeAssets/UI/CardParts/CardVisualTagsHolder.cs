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
        public Card Card { get; set; }

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
            var currentAttributes = Card.LogicalCard.CardVisualTags;

            foreach (var attr in currentAttributes)
            {
                if (!displayedAttributes.Any(item => item.ProtoSprite.SpritePath == attr.ProtoSprite.SpritePath))
                {
                    var newPrefab = TemplatePrefab.Spawn(this.transform);
                    newPrefab.CardTag = attr;
                    attr.CorrespondingPrefab = newPrefab;
                }
                /* What is this for?
                if (attr.CorrespondingPrefab != null)
                {
                    attr.CorrespondingPrefab.gameObject.Despawn();
                    attr.CorrespondingPrefab = null;
                }
                */
            }
            var childrenToPurge = new List<CardVisualTag>();
            foreach (var attr in displayedAttributes)
            {
                if (!currentAttributes.Contains(attr))
                {
                    childrenToPurge.Add(attr);
                }
            }
            this.PurgeChildrenIf<CardVisualTagPrefab>(item => childrenToPurge.Contains(item.CardTag));

        }
    }
}