// We basically use this as a sentinel value for our recursive action queue.

public class NeverEndingAction : BasicDelayedAction
{
    public NeverEndingAction() : base(() => { }, null, "MasterAction")
    {
        IsTimeoutRelevant = false;
        Parent = this;
    }

    public override bool IsFinished()
    {
        return true; //IT NEVER ENDS, but we say "true" because we never want to wait on a NeverEndingAction to perform other 
        //actions in the queue.
    }
}