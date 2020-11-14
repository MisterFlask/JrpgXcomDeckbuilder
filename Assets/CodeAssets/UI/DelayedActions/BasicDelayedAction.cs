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

public abstract class AbstractDelayedAction : BasicDelayedAction
{

    DateTime StartTime;

    public AbstractDelayedAction() : base(() => { })
    {
        onStart = () => PerformOuter();
        ActionId = GetName();
    }

    public void PerformOuter()
    {
        StartTime = DateTime.Now;
        Perform();        
    }

    public abstract string GetName();

    public abstract void Perform();

    public override bool IsFinished()
    {
        return ServiceLocator.GetActionManager().IsCurrentActionFinished || TimeoutHasOccurred();
    }

    private bool TimeoutHasOccurred()
    {
        var span = DateTime.Now - StartTime;
        if (span > Timeout)
        {
            Log.Error("Timeout occurred on action: " + ActionId);
            return true;
        }
        else
        {
            return false;
        }
    }
}