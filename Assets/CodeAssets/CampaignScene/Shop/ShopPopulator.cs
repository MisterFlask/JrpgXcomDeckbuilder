using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.CampaignScene.Shop
{
    public class ShopPopulator : MonoBehaviour
    {

        public static List<SoldierPerk> AugmeticsForSale = new List<SoldierPerk>
        {

        };

        public List<AbstractCard> CardRewardPool => EntityRegistrations.AllClassCards;


    }
}