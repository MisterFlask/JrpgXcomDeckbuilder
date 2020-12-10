using UnityEngine;
using System.Collections;

public class BuffSelfIntent : SimpleIntent
{
    public BuffSelfIntent(AbstractBattleUnit self,
        AbstractStatusEffect statusEffect): 
        base(self,
            ProtoGameSprite.FromGameIcon(path: "Sprites/weight-lifting-up", color: Color.cyan))
    {
        this.StatusEffect = statusEffect;
    }

    public AbstractStatusEffect StatusEffect { get; private set; }

    protected override void Execute()
    {
        ActionManager.Instance.ApplyStatusEffect(Source, StatusEffect);
    }
}
