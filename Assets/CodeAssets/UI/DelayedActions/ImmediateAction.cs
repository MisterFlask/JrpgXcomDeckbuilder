using UnityEngine;
using System.Collections;
using System;

public class ImmediateAction: DelayedAction
{
    public ImmediateAction(Action onStart, string name = "") : base(onStart, name)
    {
    }
    public override bool IsFinished()
    {
        return true;
    }
}
