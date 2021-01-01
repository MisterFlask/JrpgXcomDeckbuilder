using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.CampaignScene
{
    public class EntityRegistrations
    {
        public static List<AbstractSoldierClass> NonRookieSoldierClasses = new List<AbstractSoldierClass>
        {
            new VanguardSoldierClass()
        };

        public static List<AbstractCard> AllClassCards => NonRookieSoldierClasses
            .SelectMany(item => item.UniqueCardRewardPool())
            .ToList();
    }
}