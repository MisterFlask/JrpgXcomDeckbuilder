using UnityEngine;
using System.Collections;
using System;

public static class QueuedActions
{


    public static void DelayedAction(string name, Action action, QueueingType queueingType = QueueingType.TO_BACK)
    {
        if (queueingType == QueueingType.TO_BACK)
        {
            ServiceLocator.GetActionManager().actionsQueue.Add(new DelayedAction(action, name));
        }
        else
        {
            ServiceLocator.GetActionManager().actionsQueue.AddToFront(new DelayedAction(action));
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