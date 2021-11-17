using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;
using System.Collections.Generic;

public class BasicDelayedAction 
{
    public string ActionId { get; set; }
    public bool IsStarted { get; set; }

    public virtual Action onStart { get; protected set; }

    public TimeSpan Timeout = TimeSpan.FromSeconds(20); // TODO

    public List<BasicDelayedAction> ChildActionsQueue = new List<BasicDelayedAction>();

    public BasicDelayedAction Parent = null;

    public StackTrace stackTrace;

    public bool IsTimeoutRelevant = true;
    public BasicDelayedAction(Action onStart, BasicDelayedAction parent, string name = "")
    {
        stackTrace = new StackTrace();
        ActionId = name;
        this.onStart = onStart;
        if (parent != null)
        {
            Parent = parent;
            if (!parent.ChildActionsQueue.Contains(this))
            {
                parent.ChildActionsQueue.Add(this);
            }
        }
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

    public DelayedActionWithFinishTrigger(string name, Action toPerform, BasicDelayedAction parent, Func<bool> isFinished) : base(toPerform, parent)
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