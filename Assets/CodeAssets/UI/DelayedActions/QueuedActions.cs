using UnityEngine;
using System.Collections;
using System;

public static class QueuedActions
{

    /// <summary>
    /// Don't use this one, it's a pain in the ass
    /// </summary>
    public static void DelayedActionWithCustomTrigger(string name, Action action, QueueingType queueingType = QueueingType.TO_BACK)
    {
        if (queueingType == QueueingType.TO_BACK)
        {
            ServiceLocator.GetActionManager().actionsQueue.Add(new BasicDelayedAction(action, name));
        }
        else
        {
            ServiceLocator.GetActionManager().actionsQueue.AddToFront(new BasicDelayedAction(action));
        }
    }

    public static void DelayedActionWithFinishTrigger(string name, Action action, Func<bool> finishTrigger, QueueingType queueingType = QueueingType.TO_BACK)
    {
        if (queueingType == QueueingType.TO_BACK)
        {
            ServiceLocator.GetActionManager().actionsQueue.Add(new DelayedActionWithFinishTrigger(name, action, finishTrigger));
        }
        else
        {
            ServiceLocator.GetActionManager().actionsQueue.AddToFront(new DelayedActionWithFinishTrigger(name, action, finishTrigger));
        }
    }

    public static void ImmediateAction(Action action, QueueingType queueingType = QueueingType.TO_BACK)
    {
        if (queueingType == QueueingType.TO_BACK)
        {
            ServiceLocator.GetActionManager().actionsQueue.Add(new ImmediateAction(action));
        }
        else
        {
            ServiceLocator.GetActionManager().actionsQueue.AddToFront(new ImmediateAction(action));
        }
    }
}
public enum QueueingType
{
    TO_FRONT,
    TO_BACK
}