using UnityEngine;
using System.Collections;

public class BuffSelfIntent : SimpleIntent
{
    public int Stacks;

    public BuffSelfIntent(AbstractBattleUnit self,
        AbstractStatusEffect statusEffect,
        int stacks = 1): 
        base(self,
            ProtoGameSprite.FromGameIcon(path: "Sprites/fire-bowl-strength", color: Color.cyan))
    {
        Stacks = stacks;
        this.StatusEffect = statusEffect;
    }

    public AbstractStatusEffect StatusEffect { get; private set; }

    public override string GetGenericDescription()
    {
        return "This unit will buff itself next turn.";
    }

    protected override void Execute()
    {
        ActionManager.Instance.ApplyStatusEffect(Source, StatusEffect, Stacks);
    }
}
