using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public class DelayedAction 
{
    public string ActionId { get; set; }
    public bool IsStarted { get; set; }

    public Action onStart;

    public StackTrace stackTrace;
    public DelayedAction(Action onStart, string name = "")
    {
        stackTrace = new StackTrace();
        ActionId = name;
        this.onStart = onStart;
    }

    public virtual bool IsFinished()
    {
        return ServiceLocator.GetActionManager().IsCurrentActionFinished;
    }
}
