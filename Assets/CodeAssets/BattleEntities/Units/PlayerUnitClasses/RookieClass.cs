using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class RookieClass : AbstractSoldierClass
{
    public override List<AbstractCard> StartingCards()
    {
        return new List<AbstractCard>
        {
            new Defend(),
            new IneptShot(),
            new IneptShot(),
            new Defend(),
            new Bayonet()
        };
    }

    public override void LevelUpEffects(AbstractBattleUnit me)
    {
        base.LevelUpEffects(me);
        me.RemoveCardsFromPersistentDeckByType<Bayonet>();
        me.RemoveCardsFromPersistentDeckByType<Defend>();
        me.RemoveCardsFromPersistentDeckByType<IneptShot>();
        me.ChangeClass(GetRandomNewClass());

        var newClass = me.SoldierClass;
        me.AddCardsToPersistentDeck(newClass.StartingCards());
        var commonCardToAdd = newClass.UniqueCardRewardPool().Where(item => item.Rarity == Rarity.COMMON).PickRandom();
        me.AddCardsToPersistentDeck(new List<AbstractCard> {
            commonCardToAdd.CopyCard(),
            commonCardToAdd.CopyCard()
        });
        Log.Info("Added common cards to deck on promotion: 2 copies of " + commonCardToAdd.Name);
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

    public override string Name()
    {
        return "Rookie";
    }
}
