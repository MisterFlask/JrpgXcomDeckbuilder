using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rookie : AbstractAllyUnit
{
    public Rookie()
    {
        this.Name = "Rookie";

        this.IntrinsicCardsInDeck.AddRange(new List<AbstractCard>
        {
            new Grenade(),
            new CoveringFire(),
            new CoveringFire(),

        });
    }

    public override List<AbstractCard> CardsSelectableOnLevelUp()
    {
        throw new System.NotImplementedException();
    }
}
