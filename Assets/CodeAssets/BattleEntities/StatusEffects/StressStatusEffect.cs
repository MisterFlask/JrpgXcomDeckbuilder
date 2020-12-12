using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StressStatusEffect : AbstractStatusEffect
{
    
    public StressStatusEffect()
    {
        Name = "Stress";
        ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites/cracked-glass", Color.red);
    }
    public override string Description => $"If this goes above a character's maximum Hit Points," +
        $" the character gets a permanent Madness card to their " +
        $"deck and the Snapped status effect.  If this goes above a character's " +
        $"maximum hit points while Snapped, the character<color=red>dies</color>";

    public override void OnApplicationOrIncrease()
    {
        if (Stacks > OwnerUnit.MaxHp)
        {
            if (OwnerUnit.HasStatusEffect<SnappedStatusEffect>())
            {
                OwnerUnit.CurrentHp = 0;
            }
            else
            {
                Stacks = 0;
                action().ApplyStatusEffect(OwnerUnit, new SnappedStatusEffect(), 1);
                // todo: Madness card to character deck.
                action().AddCardToPersistentDeck(GetMadnessCard(), OwnerUnit);
            }
        }
    }

    /// <summary>
    /// todo: mechanic where if a character has a madness card, all subsequent ones are the same type.
    /// </summary>
    /// <returns></returns>
    public AbstractCard GetMadnessCard()
    {
        return new List<AbstractCard>
        {
            new Abusive()
        }.PickRandom();
    }
}
