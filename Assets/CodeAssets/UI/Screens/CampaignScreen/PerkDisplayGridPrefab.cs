using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.UI.Screens.CampaignScreen
{
    public class PerkDisplayGridPrefab : MonoBehaviour
    {

        public static PerkDisplayGridPrefab Instance;

        public PerkDisplayGridPrefab()
        {
        }
        public void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            Instance = this;
        }
        public PerkDisplayPrefab PerkPrefabTemplate;
        public List<AbstractSoldierPerk> PerksWeHaveRepresented = new List<AbstractSoldierPerk>();
        private List<PerkDisplayPrefab> PerksManaged  = new List<PerkDisplayPrefab>();

        public AbstractBattleUnit SoldierWhosePerksToDisplay => GameState.Instance.CharacterSelected;

        // Update is called once per frame
        void Update()
        {
            if (SoldierWhosePerksToDisplay== null)
            {
                return;
            }
            if (PerksWeHaveRepresented.EquivalentMembers(SoldierWhosePerksToDisplay.Perks))
            {
                return;
            }
            foreach(var prefab in PerksManaged)
            {
                prefab.gameObject.Despawn();
            }
            PerksManaged = new List<PerkDisplayPrefab>();

            foreach(var perk in SoldierWhosePerksToDisplay.Perks)
            {
                var newPrefab = PerkPrefabTemplate.gameObject.Spawn(this.transform).GetComponent<PerkDisplayPrefab>();
                newPrefab.Perk = perk;
                PerksManaged.Add(newPrefab);
            }

            PerksWeHaveRepresented = SoldierWhosePerksToDisplay.Perks.ToList();
        }


    }
}