﻿using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Rare
{
    public class OnwardCowards : AbstractCard
    {

        public OnwardCowards()
        {
            SoldierClassCardPools.Add(typeof(RookieClass));
            Name = "Onward, cowards!";
            TargetType = TargetType.NO_TARGET_OR_SELF;
            CardType = CardType.SkillCard;
            this.ProtoSprite = GameIconProtoSprite.FromGameIcon(path: "Sprites/bayonet");
        }

        public override string DescriptionInner()
        {
            return "Apply 10 stress to other allies.  Apply 4 Strength to other allies.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            foreach(var ally in state().AllyUnitsInBattle)
            {
                action().ApplyStress(ally, 15);
                action().ApplyStatusEffect(ally, new StrengthStatusEffect(), 4);
            }
        }
    }
}