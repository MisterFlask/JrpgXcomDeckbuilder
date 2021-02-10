using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public class BasicDelayedAction 
{
    public string ActionId { get; set; }
    public bool IsStarted { get; set; }

    public virtual Action onStart { get; protected set; }

    public TimeSpan Timeout = TimeSpan.FromSeconds(4);

    public StackTrace stackTrace;
    public BasicDelayedAction(Action onStart, string name = "")
    {
        stackTrace = new StackTrace();
        ActionId = name;
        this.onStart = onStart;
    }

    public void DeclareFinished()
    {
        ServiceLocator.GetActionManager().IsCurrentActionFinished = true;
    }

    public virtual bool IsFinished()
    {
        return ServiceLocator.GetActionManager().IsCurrentActionFinished; // this is a flag this is required to set.
    }
}


public class DelayedActionWithFinishTrigger : BasicDelayedAction
{
    Func<bool> IsFinishedFunction;

    public DelayedActionWithFinishTrigger(string name, Action toPerform, Func<bool> isFinished) : base(toPerform)
    {
        onStart = toPerform;
        ActionId = name;
        IsFinishedFunction = isFinished;
    }

    public override bool IsFinished()
    {
        return IsFinishedFunction();
    }
}