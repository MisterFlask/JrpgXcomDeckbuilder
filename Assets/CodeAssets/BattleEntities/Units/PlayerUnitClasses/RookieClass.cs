using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class RookieClass : AbstractSoldierClass
{
    public override List<AbstractCard> UniqueCardRewardPool()
    {
        return new List<AbstractCard>()
        {
            new Defend(),
            new Bayonet(),
            new CoveringFire()
        };
    }

    public override List<AbstractCard> StartingCards()
    {
        return new List<AbstractCard>
        {
            new Defend(),
            new Bayonet(),
            new Bayonet(),
            new Defend(),
            new CoveringFire()
        };
    }

    public override void LevelUpEffects(AbstractBattleUnit me)
    {
        base.LevelUpEffects(me);
        me.RemoveCardsFromPersistentDeckByType<Bayonet>();
        me.RemoveCardsFromPersistentDeckByType<CoveringFire>();
        me.ChangeClass(GetRandomNewClass());

        var newClass = me.SoldierClass;
        me.AddCardsToPersistentDeck(newClass.StartingCards());
        var commonCardToAdd = newClass.UniqueCardRewardPool().Where(item => item.Rarity == Rarity.COMMON).PickRandom();
        me.AddCardsToPersistentDeck(new List<AbstractCard> { 
            commonCardToAdd.CopyCard(),
            commonCardToAdd.CopyCard()
        });
    }

    List<AbstractSoldierClass> PromotionClasses = new List<AbstractSoldierClass>
    {
        new VanguardSoldierClass()
    };

    private AbstractSoldierClass GetRandomNewClass()
    {
        return PromotionClasses.PickRandom();
    }

    public static List<AbstractSoldierClass> GetClassesEligibleForPromotion()
    {
        return new List<AbstractSoldierClass>
        {
            new VanguardSoldierClass()
        };// todo
    }
}
