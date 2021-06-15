using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities
{
    public static class PerkAndAugmentationRegistrar
    {

        public static List<AbstractSoldierPerk> TotalPerkAndAugmentationList = new List<AbstractSoldierPerk>();

        public static AbstractSoldierPerk GetRandomPerkForNewSoldier()
        {
            throw new NotImplementedException();
        }

        public static AbstractSoldierPerk GetRandomAugmentation(Rarity rarity)
        {
            throw new NotImplementedException();
        }
    }
}