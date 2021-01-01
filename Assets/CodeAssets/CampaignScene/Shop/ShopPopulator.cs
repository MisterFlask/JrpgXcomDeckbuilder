using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.CampaignScene.Shop
{
    public class ShopPopulator : MonoBehaviour
    {

        public static List<AbstractAugmentation> AugmeticsForSale = new List<AbstractAugmentation>
        {

        };

        public List<AbstractCard> CardRewardPool => EntityRegistrations.AllClassCards;


    }
}