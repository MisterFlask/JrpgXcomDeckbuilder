using UnityEngine;
using System.Collections;
using System.Linq;

public abstract class FactionProgressEvent
{
    public string FactionName;

    public abstract int HappensAtProgressValue();
    public bool HasHappened { get; set; } = false;
    public abstract void OnTrigger();

    private Faction GetFaction => ServiceLocator.GetGameStateTracker().Factions.Where(item => item.Name == FactionName).Single();

    public void Trigger()
    {
        if (!HasHappened && HappensAtProgressValue() <= GetFaction.TotalProgress())
        {
            OnTrigger();
            HasHappened = true;
        }
    }

}
